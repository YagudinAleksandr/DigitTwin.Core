using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DigitTwin.Infrastructure.Mongo
{
    /// <inheritdoc cref="IMongoDbService"/>
    internal class MongoDbService : IMongoDbService
    {
        public IMongoDatabase Database { get; }
        public IMongoClient Client { get; }

        public MongoDbService(IOptions<MongoConfiguration> options)
        {
            var settings = MongoClientSettings.FromUrl(
                new MongoUrl(options.Value.ConnectionString)
            );

            settings.ConnectTimeout = TimeSpan.FromMilliseconds(options.Value.ConnectionTimeoutMs);
            settings.MaxConnectionPoolSize = options.Value.MaxConnectionPoolSize;
            settings.ServerSelectionTimeout = TimeSpan.FromSeconds(30);

            Client = new MongoClient(settings);
            Database = Client.GetDatabase(options.Value.DatabaseName);
        }
    }
}
