using System.Windows;
using System.Windows.Media;

namespace WOCS.UI.Dialogs
{
    /// <summary>
    /// Interaction logic for CustomDialog.xaml
    /// </summary>
    public partial class CustomDialog : Window
    {
        public CustomDialog(string title, string message)
        {
            InitializeComponent();

            TitleText.Text = title;
            MessageText.Text = message;

            // ✅ Dynamic color based on type
            switch (title)
            {
                case "Success":
                    TitleText.Foreground = Brushes.Green;
                    break;

                case "Error":
                    TitleText.Foreground = Brushes.Red;
                    break;

                case "Warning":
                    TitleText.Foreground = Brushes.Orange;
                    break;
            }


            //if (Owner != null)
            //{
            //    Owner.StateChanged += Owner_StateChanged;
            //}

        }
        //private void Owner_StateChanged(object sender, EventArgs e)
        //{
        //    if (Owner.WindowState == WindowState.Minimized)
        //    {
        //        this.WindowState = WindowState.Minimized;
        //    }
        //}

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
