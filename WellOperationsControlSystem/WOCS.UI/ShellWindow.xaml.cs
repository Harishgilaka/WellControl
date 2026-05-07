using System.Windows;
using WOCS.UI.ViewModels;

namespace WOCS.UI
{
    /// <summary>
    /// Interaction logic for ShellWindow.xaml
    /// </summary>
    public partial class ShellWindow : Window
    {
        public ShellWindow(MainViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
