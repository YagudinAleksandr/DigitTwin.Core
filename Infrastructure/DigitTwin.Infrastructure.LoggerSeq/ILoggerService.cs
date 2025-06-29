﻿namespace DigitTwin.Infrastructure.LoggerSeq
{
    /// <summary>
    /// Сервис обработки логов
    /// </summary>
    public interface ILoggerService
    {
        /// <summary>
        /// Логирование отладки
        /// </summary>
        /// <param name="message">Сообщение</param>
        void LogDebug(string service, string message);

        /// <summary>
        /// Логирование информации
        /// </summary>
        /// <param name="message">Сообщение</param>
        void LogInformation(string service, string message);

        /// <summary>
        /// Логирование предупреждения
        /// </summary>
        /// <param name="message">Сообщение</param>
        void LogWarning(string service, string message);

        /// <summary>
        /// Логирование исключения
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="ex">Исключение</param>
        void LogError(string service, string message, Exception? ex = null);
    }
}
