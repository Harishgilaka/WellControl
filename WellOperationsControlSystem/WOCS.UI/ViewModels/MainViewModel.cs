using CommunityToolkit.Mvvm.ComponentModel;
using WOCS.Application.Interfaces.Services;
using WOCS.UI.Navigation;
using WOCS.UI.Views;

namespace WOCS.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly ILoadingService _loadingService;
        private readonly INavigationService _navigationService;

        [ObservableProperty]
        private object currentView;

        [ObservableProperty]
        private object headerViewContent;

        [ObservableProperty]
        private object footerViewContent;

        // ✅ NEW global loading property
        public bool IsLoading => _loadingService.IsLoading;

        public MainViewModel(DashboardView connectionView,
           HeaderView headerView,
           FooterView footerView,
           ILoadingService loadingService,
           INavigationService navigationService)
        {
            
            _loadingService = loadingService;

            // ✅ Listen to loading changes
            _loadingService.PropertyChanged += (_, e) =>
            {
                if (e.PropertyName == nameof(_loadingService.IsLoading))
                {
                    OnPropertyChanged(nameof(IsLoading));
                }
            };

            _navigationService = navigationService;

            // ✅ Hook navigation callback
            ((NavigationService)_navigationService).Navigate = view =>
            {
                CurrentView = view;
            };

            CurrentView = connectionView;
            HeaderViewContent = headerView;
            FooterViewContent = footerView;
        }
    }
}
