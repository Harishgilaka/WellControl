using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;
using WOCS.UI.Dialogs;
using WOCS.UI.Navigation;
using WOCS.UI.Views;

namespace WOCS.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly IExproJobService _service;
        private readonly ILogger<DashboardViewModel> _logger;
        private readonly ILoadingService _loadingService;
        private readonly IExceptionLogService _exceptionLogService;
        private readonly INavigationService _navigationService;
        public ICommand ViewCommand { get; }
        public ObservableCollection<ExproJobDto> Jobs { get; } = new ObservableCollection<ExproJobDto>();
        public DashboardViewModel(IExproJobService service,
            ILogger<DashboardViewModel> logger,
            ILoadingService loadingService,
            IExceptionLogService exceptionLogService,
            INavigationService navigationService
            )
        {
            _service = service;
            _logger = logger;
            _loadingService = loadingService;
            _exceptionLogService = exceptionLogService;
            _navigationService = navigationService;
            _logger.LogInformation("DashboardViewModel initialized");
            ViewCommand = new RelayCommand<Guid>(OnView);

            _ = InitializeAsync();
        }

        private void OnView(Guid id)
        {
            _navigationService.NavigateTo<ConnectionView>(id);
        }

        private async Task InitializeAsync()
        {
            _logger.LogInformation("DashboardViewModel initialization started");

            _loadingService.Show();

            try
            {
                await LoadTopJobsAsync();

                _logger.LogInformation("DashboardViewModel initialization completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InitializeAsync failed — dispatching to UI thread");

                // ✅ Must use BeginInvoke (async) not Invoke (sync) to avoid deadlock
                //System.Windows.Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
                //{
                //    throw new Exception("DashboardViewModel initialization failed", ex);
                //}));

                // ✅ LOG INTO DATABASE
                await _exceptionLogService.LogAsync(
                    ex,
                    layer: "UI",
                    context: $"DashboardViewModel.InitializeAsync"
                );

                DialogService.ShowError("DashboardViewModel.InitializeAsync: " + ex.Message);
            }
            finally
            {
                _loadingService.Hide();
            }
        }

        public async Task LoadTopJobsAsync()
        {
            try
            {

                _logger.LogInformation("========== LoadTopJobsAsync REQUEST STARTED ==========");

                var jobs = await _service.GetJobsAsync(2); // ❌ Remove ConfigureAwait(false)
                var jobList = jobs.ToList();

                _logger.LogInformation("Response: Repository returned {JobCount} jobs", jobList.Count);

                var dispatcher = System.Windows.Application.Current?.Dispatcher;
                if (dispatcher != null)
                {
                    await dispatcher.InvokeAsync(() =>
                    {
                        Jobs.Clear();
                        foreach (var j in jobList)
                            Jobs.Add(j);
                    }).Task; // ❌ Remove ConfigureAwait(false) here too
                }
                else
                {
                    Jobs.Clear();
                    foreach (var j in jobList)
                        Jobs.Add(j);
                }

                _logger.LogInformation("========== LoadTopJobsAsync COMPLETED SUCCESSFULLY ==========");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "========== LoadTopJobsAsync FAILED ==========");

                await _exceptionLogService.LogAsync(
                 ex,
                 layer: "UI",
                 context: $"DashboardViewModel.LoadTopJobsAsync"
             );

                DialogService.ShowError("DashboardViewModel.LoadTopJobsAsync: " + ex.Message);
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
