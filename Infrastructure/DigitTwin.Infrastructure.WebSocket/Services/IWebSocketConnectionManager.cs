using System.Collections.Generic;
using System.Net.WebSockets;

namespace DigitTwin.Infrastructure.WS
{
    /// <summary>
    /// Сервис отслеживания соединений
    /// </summary>
    public interface IWebSocketConnectionManager
    {
        /// <summary>
        /// Добавление соединение
        /// </summary>
        /// <param name="socket">WebSocket</param>
        /// <param name="id">ИД</param>
        void AddSocket(WebSocket socket, string id);

        /// <summary>
        /// Удаление соединения
        /// </summary>
        /// <param name="id">ИД</param>
        void RemoveSocket(string id);

        /// <summary>
        /// Получение соединения по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Соединение</returns>
        WebSocket GetSocketById(string id);

        /// <summary>
        /// Получение списка соединений
        /// </summary>
        /// <returns>Список соединений</returns>
        IEnumerable<WebSocket> GetAllSockets();

        /// <summary>
        /// Получение ИД соединения
        /// </summary>
        /// <param name="socket">Соединение</param>
        /// <returns>ИД</returns>
        string GetId(WebSocket socket);
    }
}
