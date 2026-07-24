using System.Windows.Controls;
using WOCS.UI.ViewModels;

namespace WOCS.UI.Views
{
    /// <summary>
    /// Interaction logic for DeviceDashboardView.xaml
    /// </summary>
    public partial class DeviceDashboardView : UserControl
    {
        public DeviceDashboardView(DeviceDashboardViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}
