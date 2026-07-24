using System.Windows.Controls;
using WOCS.UI.ViewModels;

namespace WOCS.UI.Views
{
    /// <summary>
    /// Interaction logic for ConnectionView.xaml
    /// </summary>
    public partial class ConnectionView : UserControl
    {
        public ConnectionView(ConnectionViewModel connectionViewModel)
        {
            InitializeComponent();
            DataContext = connectionViewModel;
        }
    }
}
