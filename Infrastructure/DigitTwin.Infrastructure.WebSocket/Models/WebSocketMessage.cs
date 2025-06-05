namespace DigitTwin.Infrastructure.WS
{
    /// <summary>
    /// Содель сообщения для WebSocket
    /// </summary>
    public class WebSocketMessage
    {
        /// <summary>
        /// Тип сообщения
        /// </summary>
        public string MessageType { get; set; } = string.Empty;

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Data { get; set; } = string.Empty;
    }
}
