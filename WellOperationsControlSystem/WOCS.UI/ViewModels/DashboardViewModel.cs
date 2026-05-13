using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using WOCS.Application.Interfaces.Services;
using WOCS.Domain.Entities;

namespace WOCS.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly IExproJobService _service;
        private readonly ILogger<DashboardViewModel> _logger;
        private bool _isLoading;

        public ObservableCollection<ExproJobDto> Jobs { get; } = new ObservableCollection<ExproJobDto>();

        public bool IsLoading
        {
            get => _isLoading;
            private set
            {
                if (_isLoading == value) return;
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public DashboardViewModel(IExproJobService service, ILogger<DashboardViewModel> logger)
        {
            _service = service;
            _logger = logger;
            _logger.LogInformation("DashboardViewModel initialized");
            _ = InitializeAsync();
        }


        private async Task InitializeAsync()
        {
            _logger.LogInformation("DashboardViewModel initialization started");
            try
            {
                await LoadTopJobsAsync();
                _logger.LogInformation("DashboardViewModel initialization completed");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "InitializeAsync failed — dispatching to UI thread");

                // ✅ Must use BeginInvoke (async) not Invoke (sync) to avoid deadlock
                System.Windows.Application.Current?.Dispatcher.BeginInvoke(new Action(() =>
                {
                    throw new Exception("DashboardViewModel initialization failed", ex);
                }));
            }
        }

        public async Task LoadTopJobsAsync()
        {
            try
            {
                _logger.LogInformation("========== LoadTopJobsAsync REQUEST STARTED ==========");

                IsLoading = true;
                var jobs = await _service.GetJobsAsync(10); // ❌ Remove ConfigureAwait(false)
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
                throw; // ✅ rethrow to InitializeAsync catch block
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
