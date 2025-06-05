using System.Threading.Tasks;

namespace DigitTwin.Infrastructure.WS
{
    /// <summary>
    /// Сервис для создания WebSocket соединения
    /// </summary>
    public interface IWebSocketServer
    {
        /// <summary>
        /// Отправка сообщения
        /// </summary>
        /// <typeparam name="T">Тип моделит сообщения</typeparam>
        /// <param name="connectionId">ИД соединения</param>
        /// <param name="message">Сообзение</param>
        Task SendMessageAsync<T>(string connectionId, T message);

        /// <summary>
        /// Широковещательная отправка сообщений
        /// </summary>
        /// <typeparam name="T">Тип модели сообщений</typeparam>
        /// <param name="message">Сообщение</param>
        Task BroadcastAsync<T>(T message);
    }
}
