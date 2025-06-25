using MongoDB.Driver;

namespace DigitTwin.Infrastructure.Mongo
{
    /// <summary>
    /// Сервис для работы с MongoDB
    /// </summary>
    public interface IMongoDbService
    {
        /// <summary>
        /// База MongoDB
        /// </summary>
        IMongoDatabase Database { get; }

        /// <summary>
        /// Клиент
        /// </summary>
        IMongoClient Client { get; }
    }
}
