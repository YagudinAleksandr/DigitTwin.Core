namespace DigitTwin.Infrastructure.Mongo
{
    /// <summary>
    /// Конфигурация MongoDB
    /// </summary>
    public class MongoConfiguration
    {
        /// <summary>
        /// Название секции настроек
        /// </summary>
        public const string SectionName = "MongoDbConnection";

        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; set; } = "mongodb://localhost:27017";

        /// <summary>
        /// Название базы данных
        /// </summary>
        public string DatabaseName { get; set; } = "defaultDb";

        /// <summary>
        /// Time-out соединения
        /// </summary>
        public int ConnectionTimeoutMs { get; set; } = 30000;

        /// <summary>
        /// 
        /// </summary>
        public int MaxConnectionPoolSize { get; set; } = 100;
    }
}
