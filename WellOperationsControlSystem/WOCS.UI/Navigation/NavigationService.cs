using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WOCS.Application.Interfaces.ViewModels;
using WOCS.UI.Views;

namespace WOCS.UI.Navigation
{
    public class NavigationService : INavigationService
    {
        private readonly IServiceProvider _serviceProvider;

        // ✅ Delegate used by MainWindow
        public Action<object>? Navigate { get; set; }

        // ✅ Navigation history stack
        private readonly Stack<object> _history = new();

        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        // ============================
        // ✅ NAVIGATE TO DASHBOARD
        // ============================
        public void NavigateToDashboard()
        {
            NavigateTo<DashboardView>();
        }

        // ============================
        // ✅ NAVIGATE TO CONNECTION
        // ============================
        //public void NavigateToConnection(Guid jobId)
        //{
        //    var view = _serviceProvider.GetRequiredService<ConnectionView>();

        //    if (view.DataContext is ConnectionViewModel vm)
        //    {
        //        vm.SetJobId(jobId);
        //    }

        //    NavigateInternal(view);
        //}

        public void NavigateTo<TView>(Guid jobId) where TView : FrameworkElement
        {

            var view = _serviceProvider.GetRequiredService<TView>();

            if (view.DataContext is IJobViewModel vm)
            {
                vm.SetJobId(jobId);   
            }

            NavigateInternal(view);
        }

        // ============================
        // ✅ GENERIC NAVIGATION
        // ============================
        private void NavigateTo<TView>() where TView : FrameworkElement
        {
            var view = _serviceProvider.GetRequiredService<TView>();
            NavigateInternal(view);
        }

        private void NavigateInternal(object view)
        {
            // Save current view before navigation
            if (_currentView != null)
            {
                _history.Push(_currentView);
            }

            _currentView = view;

            Navigate?.Invoke(view);
        }

        // ✅ Keep track of current view
        private object? _currentView;

        // ============================
        // ✅ GO BACK IMPLEMENTATION
        // ============================
        public void GoBack()
        {
            if (_history.Count > 0)
            {
                var previousView = _history.Pop();
                _currentView = previousView;

                Navigate?.Invoke(previousView);
            }
        }
    }
}