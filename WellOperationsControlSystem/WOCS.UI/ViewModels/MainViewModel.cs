using CommunityToolkit.Mvvm.ComponentModel;
using WOCS.UI.Views;

namespace WOCS.UI.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private object currentView;

        [ObservableProperty]
        private object headerViewContent;

        [ObservableProperty]
        private object footerViewContent;

        public MainViewModel(DashboardView dashboardView, HeaderView headerView, FooterView footerView)
        {
            CurrentView = dashboardView;
            HeaderViewContent = headerView;
            FooterViewContent = footerView;
        }
    }
}
