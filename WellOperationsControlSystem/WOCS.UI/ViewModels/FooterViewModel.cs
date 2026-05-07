using CommunityToolkit.Mvvm.ComponentModel;

namespace WOCS.UI.ViewModels
{
    public class FooterViewModel : ObservableObject
    {

        private int _currentYear;
        public int CurrentYear
        {
            get => _currentYear;
            set => SetProperty(ref _currentYear, value);
        }

        public FooterViewModel()
        {
            CurrentYear = DateTime.UtcNow.Year;
        }
    }
}
