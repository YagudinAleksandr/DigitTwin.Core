namespace DigitTwin.Infrastructure.LoggerSeq
{
    /// <summary>
    /// Конфигурация логгера
    /// </summary>
    public class LoggerConfig
    {
        /// <summary>
        /// Строка подключения к SEQ
        /// </summary>
        public string Url { get; set; } = null!;

        /// <summary>
        /// Минимальный уровень логирования
        /// </summary>
        public string MinLevel { get; set; } = "Information";

        /// <summary>
        /// Название приложения
        /// </summary>
        public string ApplicationName { get; set; } = null!;
    }
}
