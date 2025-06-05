using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DigitTwin.Infrastructure.WS
{
    /// <inheritdoc cref="IWebSocketServer"/>
    public class WebSocketServer : IWebSocketServer
    {
        #region CTOR
        /// <inheritdoc cref="IWebSocketConnectionManager"/>
        private readonly IWebSocketConnectionManager _manager;

        public WebSocketServer(IWebSocketConnectionManager manager)
        {
            _manager = manager;
        }
        #endregion

        public async Task BroadcastAsync<T>(T message)
        {
            var json = JsonSerializer.Serialize(message);
            var bytes = Encoding.UTF8.GetBytes(json);

            foreach (var socket in _manager.GetAllSockets())
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }
            }
        }

        public async Task SendMessageAsync<T>(string connectionId, T message)
        {
            var socket = _manager.GetSocketById(connectionId);
            if (socket?.State == WebSocketState.Open)
            {
                var json = JsonSerializer.Serialize(message);
                var bytes = Encoding.UTF8.GetBytes(json);
                await socket.SendAsync(
                    new ArraySegment<byte>(bytes),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
        }
    }
}
