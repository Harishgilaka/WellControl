using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using WOCS.Infrastructure.Data;
using WOCS.Infrastructure.Interfaces;

namespace WOCS.UI.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly IExproJobRepository _repository;
        private readonly ILogger<DashboardViewModel> _logger;
        private bool _isLoading;

        public ObservableCollection<ExproJob> Jobs { get; } = new ObservableCollection<ExproJob>();

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

        public DashboardViewModel(IExproJobRepository repository, ILogger<DashboardViewModel> logger)
        {
            _repository = repository;
            _logger = logger;
            _logger.LogInformation("DashboardViewModel initialized");
            _ = InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            _logger.LogInformation("DashboardViewModel initialization started");
            await LoadTopJobsAsync();
            _logger.LogInformation("DashboardViewModel initialization completed");
        }

        public async Task LoadTopJobsAsync()
        {
            try
            {
                _logger.LogInformation("========== LoadTopJobsAsync REQUEST STARTED ==========");
                _logger.LogInformation("Request: Fetching top 10 jobs from repository");
                _logger.LogDebug("Request Time: {RequestTime}", DateTime.UtcNow);

                IsLoading = true;
                var jobs = await _repository.GetTop10Async().ConfigureAwait(false);

                _logger.LogInformation("========== LoadTopJobsAsync RESPONSE RECEIVED ==========");
                _logger.LogInformation("Response: Repository returned {JobCount} jobs", jobs.Count);
                _logger.LogDebug("Response Time: {ResponseTime}", DateTime.UtcNow);

                if (jobs.Count == 0)
                {
                    _logger.LogWarning("No jobs found in repository response");
                }

                // marshal collection updates to the UI thread safely
                var dispatcher = System.Windows.Application.Current?.Dispatcher;
                if (dispatcher != null)
                {
                    _logger.LogDebug("Marshaling job collection updates to UI thread");
                    // Dispatcher.InvokeAsync returns a DispatcherOperation which exposes a Task we can await
                    await dispatcher.InvokeAsync(() =>
                    {
                        Jobs.Clear();
                        foreach (var j in jobs)
                            Jobs.Add(j);
                        _logger.LogInformation("Jobs collection updated on UI thread with {JobCount} items", Jobs.Count);
                    }).Task.ConfigureAwait(false);
                }
                else
                {
                    // Fallback: update directly (only safe in some hostings)
                    _logger.LogWarning("Dispatcher not available, updating jobs directly");
                    Jobs.Clear();
                    foreach (var j in jobs)
                        Jobs.Add(j);
                    _logger.LogInformation("Jobs collection updated directly with {JobCount} items", Jobs.Count);
                }

                _logger.LogInformation("========== LoadTopJobsAsync COMPLETED SUCCESSFULLY ==========");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "========== LoadTopJobsAsync FAILED ==========");
                _logger.LogError("Error occurred while loading top jobs from repository");
                throw;
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
