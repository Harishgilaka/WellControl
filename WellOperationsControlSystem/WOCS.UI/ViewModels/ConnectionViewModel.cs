using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using WOCS.Application.Interfaces.Services;
using WOCS.Application.Interfaces.ViewModels;
using WOCS.UI.Dialogs;
using WOCS.UI.Navigation;
using WOCS.UI.Views;

namespace WOCS.UI.ViewModels
{
    public class ConnectionViewModel : INotifyPropertyChanged, IJobViewModel
    {
        private readonly ITcpCommunicationService _tcpService;
        private readonly IExceptionLogService _exceptionLogService;
        private readonly ILoadingService _loadingService;
        private readonly INavigationService _navigationService;
        public string IpAddress { get; set; } = "192.168.10.1";
        public int Port { get; set; } = 2000;
        public bool IsConnected => _tcpService.IsConnected;
        public string ConnectionStatus => IsConnected ? "Connected ✅" : "Disconnected ❌";
        public ICommand ConnectCommand { get; }
        public ICommand DisconnectCommand { get; }
        public ICommand BackCommand { get; }
        private Guid _jobId;
        public ConnectionViewModel(ITcpCommunicationService tcpService,
            IExceptionLogService exceptionLogService,
            ILoadingService loadingService,
            INavigationService navigationService)
        {
            _tcpService = tcpService;
            _exceptionLogService = exceptionLogService;
            _loadingService = loadingService;
            _navigationService = navigationService;
            BackCommand = new RelayCommand(BackNavigation);
            ConnectCommand = new RelayCommand(async () => await ConnectAsync());
            DisconnectCommand = new RelayCommand(async () => await DisconnectAsync());
        }
        private void BackNavigation()
        {
            _navigationService.NavigateToDashboard();
        }
        public void SetJobId(Guid jobId)
        {
            _jobId = jobId;
        }
        private async Task ConnectAsync()
        {
            try
            {
                _loadingService.Show();   // ✅
                var isConnected = await _tcpService.ConnectAsync(IpAddress, Port);

                _loadingService.Hide();

                if (isConnected)
                {
                    DialogService.ShowSuccess("Connected successfully");

                    _navigationService.NavigateTo<DeviceDashboardView>(_jobId);
                }
                else
                {
                    DialogService.ShowError("Connection failed");
                }


                // ✅ Send something

                //// ✅ Build IWIS payload
                //var payload = new List<byte>
                //        {
                //            0x02, // Query

                //            0x00, 0x01, 0x00, // Assembly
                //            0x01, 0x01, 0x00  // Device
                //        };


                //// ✅ Build frame
                //byte[] frame = IwisPacket.BuildIwisFrame(payload.ToArray());

                //await _tcpService.SendAsync(frame);

                //// ✅ Receive response
                //var responseFrame = await _tcpService.ReceiveAsync();

                //// ✅ Extract payload
                //var payloadResponse = IwisPacket.ExtractPayload(responseFrame);

                //if (payloadResponse.Length == 0)
                //    throw new Exception("Connected but no response from server");


                //DialogService.ShowSuccess("Connected successfully");
            }
            catch (Exception ex)
            {
                // ✅ LOG INTO DATABASE
                //await _exceptionLogService.LogAsync(
                //    ex,
                //    layer: "UI",
                //    context: $"ConnectionViewModel.ConnectAsync | IP:{IpAddress} Port:{Port}"
                //);

                DialogService.ShowError("Connection failed: " + ex.Message);
            }
            finally
            {
                //_loadingService.Hide();  // ✅

                OnPropertyChanged(nameof(IsConnected));
                OnPropertyChanged(nameof(ConnectionStatus));
            }
        }
        private async Task DisconnectAsync()
        {
            await _tcpService.DisconnectAsync();

            OnPropertyChanged(nameof(IsConnected));
            OnPropertyChanged(nameof(ConnectionStatus));

            DialogService.ShowWarning("Disconnected");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = "")
                => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
