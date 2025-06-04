namespace DigitTwin.Infrastructure.DatabaseContext
{
    /// <summary>
    /// Конфигурация подключения к БД
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Тип подключения
        /// </summary>
        public DatabaseType Type { get; set; } = DatabaseType.PostgreSQL;
    }
}
