using System.Windows;

namespace WOCS.UI.Navigation
{
    public interface INavigationService
    {
        void NavigateToDashboard();
        //void NavigateToConnection(Guid jobId);
        void GoBack();
        void NavigateTo<TView>(Guid jobId) where TView : FrameworkElement;
    }
}
