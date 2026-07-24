using System.ComponentModel;

namespace WOCS.Application.Interfaces.Services
{
    public interface ILoadingService : INotifyPropertyChanged
    {
        bool IsLoading { get; set; }

        void Show();
        void Hide();
    }
}
