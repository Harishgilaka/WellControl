namespace WOCS.UI.Dialogs
{
    public static class DialogService
    {

        public static void ShowError(string message)
        {
            Show("Error", message);
        }

        public static void ShowWarning(string message)
        {
            Show("Warning", message);
        }

        public static void ShowSuccess(string message)
        {
            Show("Success", message);
        }

        public static void Show(string title, string message)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var dialog = new CustomDialog(title, message)
                {
                    Owner = System.Windows.Application.Current.MainWindow
                };

                dialog.ShowDialog();
            });
        }

    }
}
