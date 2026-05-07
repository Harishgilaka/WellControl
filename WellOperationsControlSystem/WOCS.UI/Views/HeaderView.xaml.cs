using System.Windows.Controls;
using WOCS.UI.ViewModels;

namespace WOCS.UI.Views
{
    /// <summary>
    /// Interaction logic for HeaderView.xaml
    /// </summary>
    public partial class HeaderView : UserControl
    {
        public HeaderView(HeaderViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
