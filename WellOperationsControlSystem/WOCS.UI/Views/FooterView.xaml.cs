using System.Windows.Controls;
using WOCS.UI.ViewModels;

namespace WOCS.UI.Views
{
    /// <summary>
    /// Interaction logic for FooterView.xaml
    /// </summary>
    public partial class FooterView : UserControl
    {
        public FooterView(FooterViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
