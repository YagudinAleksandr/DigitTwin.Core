namespace DigitTwin.Infrastructure.Redis
{
    /// <summary>
    /// Конфигурация Redis
    /// </summary>
    public class RedisConfiguration
    {
        /// <summary>
        /// Секция настроек
        /// </summary>
        public const string SectionName = "RedisSettings";

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString {  get; set; } = string.Empty;

        /// <summary>
        /// Базовая настройка жизни записи
        /// </summary>
        /// <remarks>5 минут по умеолчанию</remarks>
        public int DefaultTTLSeconds { get; set; } = 300;
    }
}
