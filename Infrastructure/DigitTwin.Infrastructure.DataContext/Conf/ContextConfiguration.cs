namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Конфигурация подключения к БД
    /// </summary>
    internal class ContextConfiguration
    {
        /// <summary>
        /// Тип подключения
        /// <list type="bullet">
        /// <item><description>NPG - PostgreSQL</description></item>
        /// <item><description>SQL - MS SQL</description></item>
        /// </list>
        /// </summary>
        public string Type { get; set; } = "NPG";

        /// <summary>
        /// Строка подулючения
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;
    }
}
