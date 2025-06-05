using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DigitTwin.Infrastructure.WS
{
    /// <summary>
    /// Middleware для соединения WebSocket
    /// </summary>
    public class WebSocketMiddleware
    {
        #region CTOR
        /// <inheritdoc cref="RequestDelegate"/>
        private readonly RequestDelegate _next;

        /// <inheritdoc cref="IWebSocketConnectionManager"/>
        private readonly IWebSocketConnectionManager _manager;

        public WebSocketMiddleware(RequestDelegate next,
            IWebSocketConnectionManager manager)
        {
            _next = next;
            _manager = manager;
        }
        #endregion

        /// <summary>
        /// Асинхронное выполнение HTTP контекста
        /// </summary>
        /// <param name="context">HTTP контекст</param>
        public async Task InvokeAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket socket = await context.WebSockets.AcceptWebSocketAsync();
                string connectionId = Guid.NewGuid().ToString();
                _manager.AddSocket(socket, connectionId);

                await ReceiveMessages(socket, connectionId);
            }
            else
            {
                await _next(context);
            }
        }

        /// <summary>
        /// Обработка полученого сообщения
        /// </summary>
        /// <param name="socket">WebSocket соединение</param>
        /// <param name="connectionId">ИД соединения</param>
        private async Task ReceiveMessages(WebSocket socket, string connectionId)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await socket.CloseAsync(
                        WebSocketCloseStatus.NormalClosure,
                        null,
                        CancellationToken.None);
                    _manager.RemoveSocket(connectionId);
                    break;
                }
            }
        }
    }
}
