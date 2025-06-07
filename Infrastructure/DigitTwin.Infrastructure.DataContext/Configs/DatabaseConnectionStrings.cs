namespace DigitTwin.Infrastructure.DataContext
{
    /// <summary>
    /// Строки подключения к базам данных
    /// </summary>
    public class DatabaseConnectionStrings
    {
        /// <summary>
        /// Подключение к PostgreSQL
        /// </summary>
        public string PostgreSQL { get; set; } = null!;

        /// <summary>
        /// Подключение к MS SQL
        /// </summary>
        public string MSSQL { get; set; } = null!;
    }
}
