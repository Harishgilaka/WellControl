using System.ComponentModel;
using System.Runtime.CompilerServices;
using WOCS.Application.Interfaces.Services;

namespace WOCS.UI.Services
{
    public class LoadingService : ILoadingService, INotifyPropertyChanged
    {
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public void Show() => IsLoading = true;
        public void Hide() => IsLoading = false;


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
