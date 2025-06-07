namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Настройки подключения к базам данных
    /// </summary>
    public class DatabaseSettings
    {
        /// <summary>
        /// Название секции в appsettings.json
        /// </summary>
        public const string SectionName = "DatabaseSettings";

        /// <summary>
        /// Провайдер
        /// </summary>
        public string Provider { get; set; } = null!;

        /// <summary>
        /// Строки подключения
        /// </summary>
        public DatabaseConnectionStrings ConnectionStrings { get; set; } = null!;
    }
}
