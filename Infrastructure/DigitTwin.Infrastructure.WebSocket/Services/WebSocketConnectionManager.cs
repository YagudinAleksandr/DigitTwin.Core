using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;

namespace DigitTwin.Infrastructure.WS
{
    /// <inheritdoc cref="IWebSocketConnectionManager"/>
    internal class WebSocketConnectionManager : IWebSocketConnectionManager
    {
        /// <summary>
        /// Список соединений
        /// </summary>
        private readonly ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public void AddSocket(WebSocket socket, string id)
        {
            _sockets.TryAdd(id, socket);
        }

        public IEnumerable<WebSocket> GetAllSockets()
        {
            return _sockets.Values;
        }

        public string GetId(WebSocket socket)
        {
            return _sockets.FirstOrDefault(x => x.Value == socket).Key;
        }

        public WebSocket GetSocketById(string id)
        {
            _sockets.TryGetValue(id, out var socket);
            return socket;
        }

        public void RemoveSocket(string id)
        {
            _sockets.TryRemove(id, out _);
        }
    }
}
