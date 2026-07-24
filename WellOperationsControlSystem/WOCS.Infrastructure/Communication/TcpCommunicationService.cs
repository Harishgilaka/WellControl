using System.Net;
using System.Net.Sockets;
using WOCS.Application.Interfaces.Services;

namespace WOCS.Infrastructure.Communication
{
    public class TcpCommunicationService : ITcpCommunicationService
    {
        private TcpClient _client;
        private NetworkStream _stream;

        public bool IsConnected => _client?.Connected ?? false;

        public async Task<bool> ConnectAsync(string ipAddress, int port)
        {
            try
            {
                _client = new TcpClient();
                await _client.ConnectAsync(IPAddress.Parse(ipAddress), port);
                _stream = _client.GetStream();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task DisconnectAsync()
        {
            _stream?.Close();
            _client?.Close();

            await Task.CompletedTask;
        }

        public async Task SendAsync(byte[] data)
        {
            if (!IsConnected)
                throw new Exception("Not connected");

            await _stream.WriteAsync(data);
        }

        public async Task<byte[]> ReceiveAsync()
        {
            if (!IsConnected || _stream == null)
                throw new Exception("Not connected");

            var buffer = new byte[4096];

            // ✅ Convert ValueTask → Task
            var readTask = _stream.ReadAsync(buffer).AsTask();

            if (await Task.WhenAny(readTask, Task.Delay(15000)) != readTask)
                throw new Exception("No response from server (timeout)");

            int bytesRead = await readTask;

            return buffer[..bytesRead];
        }
    }
}
