using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WOCS.Application.Interfaces.Services;
using WOCS.Application.Interfaces.ViewModels;
using WOCS.Common;
using WOCS.Domain.Entities;
using WOCS.Domain.Enums;
using WOCS.Infrastructure.Data;
using WOCS.UI.Navigation;
using LynxDeviceGroup = WOCS.Domain.Enums.LynxDeviceGroup;

namespace WOCS.UI.ViewModels
{
    public partial class DeviceDashboardViewModel : INotifyPropertyChanged, IJobViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly ILynxAssemblyScheduleService _lynxAssemblyScheduleService;
        private readonly ILynxOperationService _lynxOperationService;
        private readonly ILynxOperationVersionService _lynxOperationVersionService;
        private readonly IChirpFrequencyRangeService _chirpFrequencyRangeService;

        public ICommand BackCommand { get; }
        public ICommand OpenCommand { get; }
        public ICommand CloseCommand { get; }
        private Guid _jobId;
        public string ValveName { get; set; }
        public string LastReceived { get; set; }
        public string ReportedPosition { get; set; }
        public int RemainingActivations { get; set; }
        public string BatteryHealth { get; set; }
        public string TransmissionStatusLine1 { get; set; }
        public string TransmissionStatusLine2 { get; set; } = string.Empty;

        // ✅ CONSTRUCTOR
        public DeviceDashboardViewModel(INavigationService navigationService,
            ILynxAssemblyScheduleService lynxAssemblyScheduleService,
            ILynxOperationService lynxOperationService,
            ILynxOperationVersionService lynxOperationVersionService,
            IChirpFrequencyRangeService chirpFrequencyRangeService)
        {
            _navigationService = navigationService;
            _lynxAssemblyScheduleService = lynxAssemblyScheduleService;
            _lynxOperationService = lynxOperationService;
            _lynxOperationVersionService = lynxOperationVersionService;


            // Temporary default values (replace with service data later)
            ValveName = "Valve 1 - 1000m";
            LastReceived = "14/10/25 13:41:15";
            ReportedPosition = "OPEN";
            RemainingActivations = 9;
            BatteryHealth = "89%";
            TransmissionStatusLine1 = "Close Command queued until 16/10/25 14:30:00";
            TransmissionStatusLine2 = "Transmitting in 03:37:05";
            BackCommand = new RelayCommand(async () => await BackNavigation());
            OpenCommand = new RelayCommand(async () => await Open());
            CloseCommand = new RelayCommand(async () => await Close());
            _chirpFrequencyRangeService = chirpFrequencyRangeService;
        }

        // 🔹 Back Navigation
        //[RelayCommand]
        private async Task BackNavigation()
        {
            _navigationService.GoBack();
        }

        public void SetJobId(Guid jobId)
        {
            _jobId = jobId;

            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            await LoadLynxOperationDataAsync();
        }

        public async Task<ObservableCollection<Appointment>> LoadLynxOperationDataAsync()
        {
            var schedule = new ObservableCollection<Appointment>();
            var emDownholeSensorName = LynxDeviceGroup.EMDownholeSensor.DisplayName();
            var operations = await _lynxOperationService.GetOperationsWithJobIdAsync(_jobId);
            var chirpFrequencyRange = await _chirpFrequencyRangeService.GetAllChirpFrequencyRangeAsync();

            foreach (var operation in operations)
            {
                var operationVersions = await _lynxOperationVersionService
                    .GetAllOperationVersionsWithOperationIdAsync(operation.Id);

                var stationsById = operationVersions
                    .SelectMany(v => v.Stations)
                    .ToDictionary(s => s.Id);

                var orderedDownholeSensorAssemblies = stationsById.Values
                    .OrderBy(s => s.Position)                              // station order first
                    .SelectMany(s => s.Assemblies
                        .Where(a => a.Name == emDownholeSensorName)
                        .OrderBy(a => a.Position))                          // then assembly order within station
                    .ToList();

                foreach (var assembly in orderedDownholeSensorAssemblies)
                {
                    var station = stationsById[assembly.StationId];
                    double chirpFrequencyDuration = GetDurationById(chirpFrequencyRange, assembly.AssemblyDevices?.FirstOrDefault()?.LynxChirpFrequencyRangeId ?? 0);
                    AppendScheduleBlocks(assembly, station, schedule, chirpFrequencyDuration);
                }
            }

            return schedule;
        }

        private void AppendScheduleBlocks(
            AssemblyDto assembly,
            StationDto station,
            ObservableCollection<Appointment> schedule, double chirpFrequencyDuration)
        {
            DateTime? scheduleTime = assembly.ScheduleStartTime;

            var actionBlocks = assembly.lynxAssemblyScheduleActionBlock
                .Where(b => b.ActionBlockTypeId != (int)LynxScheduleActionsTypeEnum.BaseActionBlock)
                .OrderBy(b => b.BlockNumber)
                .ToList();

            foreach (var actionBlock in actionBlocks.OrderBy(b => b.BlockNumber))
            {
                if (scheduleTime == null)
                    continue; // nothing to schedule against

                scheduleTime = actionBlock.ActionBlockTypeId switch
                {
                    (int)LynxScheduleActionsTypeEnum.WaitForAction =>
                        HandleWaitForAction(actionBlock, scheduleTime.Value, assembly, schedule),

                    (int)LynxScheduleActionsTypeEnum.QueryHistoricDataAction =>
                        HandleQueryHistoricDataAction(actionBlock, scheduleTime.Value, station, assembly, schedule, chirpFrequencyDuration),

                    _ => scheduleTime
                };
            }
        }

        private DateTime? HandleWaitForAction(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime scheduleTime,
            AssemblyDto assembly,
            ObservableCollection<Appointment> schedule)
        {
            var waitForApp = new ActionBlockWaitForSchedule(actionBlock, scheduleTime);
            schedule.Add(waitForApp);

            return waitForApp.End.AddMinutes((double)actionBlock.DurationTs.TotalMinutes);
        }

        private DateTime? HandleQueryHistoricDataAction(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime scheduleTime,
            StationDto station,
            AssemblyDto assembly,
            ObservableCollection<Appointment> schedule, double chirpFrequencyDuration)
        {
            actionBlock.TimeOfFlight = TimeofFlight.GetTimeofFlight(
                actionBlock.DurationTs,
                actionBlock.DataIntervalTs,
                station.Position,
                actionBlock.DataFormat,
                chirpFrequencyDuration);

            var app = new ActionBlockQueryForSchedule(actionBlock, scheduleTime);
            schedule.Add(app);

            if (actionBlock.RepeatIndefinetly)
            {
                AppendRepeats(actionBlock, scheduleTime, assembly, schedule, repeatCount: 100);
            }
            else if (actionBlock.NumberOfRepeats > 0)
            {
                AppendRepeats(actionBlock, scheduleTime, assembly, schedule, actionBlock.NumberOfRepeats);
            }

            return schedule.LastOrDefault()?.End;
        }

        private void AppendRepeats(
            LynxAssemblyScheduleActionBlockDto actionBlock,
            DateTime scheduleTime,
            AssemblyDto assembly,
            ObservableCollection<Appointment> schedule,
            int repeatCount)
        {
            var interval = (double)actionBlock.TransmissionIntervalTs.TotalMinutes;

            for (var i = 0; i < repeatCount; i++)
            {
                scheduleTime = scheduleTime.AddMinutes(interval);

                var recursiveApp = new ActionBlockQueryForSchedule(actionBlock, scheduleTime);
                schedule.Add(recursiveApp);
            }
        }

        public double GetDurationById(IEnumerable<ChirpFrequencyRangeDto> ranges, int id, double defaultValue = 6.75)
        {
            var item = ranges?.FirstOrDefault(x => x.Id == id);
            return item?.Duration ?? defaultValue;
        }

        // 🔹 Open Command (Value Controller)
        private async Task Open()
        {
            // TODO: Call control service
            ReportedPosition = "OPEN";
            OnPropertyChanged(nameof(ReportedPosition));

            //Guid assemblyId = Guid.Parse("5581101A-AD23-46A6-8E15-F2ECF002B8AD");

            //var xxx = await _lynxAssemblyScheduleService.GetActionBlockByIdAsync(assemblyId);
            //var a = xxx;
        }

        // 🔹 Close Command (Value Controller)
        private async Task Close()
        {
            // TODO: Call control service
            ReportedPosition = "CLOSED";
            OnPropertyChanged(nameof(ReportedPosition));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
               => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}