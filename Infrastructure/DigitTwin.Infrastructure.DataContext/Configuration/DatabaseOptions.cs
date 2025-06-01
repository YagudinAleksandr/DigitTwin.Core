namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Настройки подключения к БД
    /// </summary>
    internal class DatabaseOptions
    {
        /// <summary>
        /// Тип базы данных
        /// <list type="bullet">
        /// <item>NPG - PostgreSQL</item>
        /// <item>MSQ - MS SQL</item>
        /// </list>
        /// </summary>
        public string DbType { get; set; } = "NPG"; // "NPG" или "MSQ"

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
