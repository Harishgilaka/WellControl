namespace WOCS.Application.Interfaces.Services
{
    public interface ITcpCommunicationService
    {
        Task<bool> ConnectAsync(string ipAddress, int port);
        Task DisconnectAsync();
        bool IsConnected { get; }
        Task SendAsync(byte[] data);
        Task<byte[]> ReceiveAsync();
    }
}
