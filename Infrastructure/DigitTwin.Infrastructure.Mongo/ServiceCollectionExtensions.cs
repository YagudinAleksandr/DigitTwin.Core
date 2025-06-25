using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DigitTwin.Infrastructure.Mongo
{
    /// <summary>
    /// Расширения для подключения к MongoDB
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Подключение к MongoDB
        /// </summary>
        /// <param name="services">DI</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>DI</returns>
        public static IServiceCollection AddMongoDb(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // Биндим конфигурацию напрямую
            var mongoConfig = new MongoConfiguration();
            configuration.GetSection(MongoConfiguration.SectionName).Bind(mongoConfig);

            // Регистрируем конфигурацию как синглтон
            services.AddSingleton(mongoConfig);

            // Регистрация клиента
            services.AddSingleton<IMongoClient>(provider =>
            {
                var config = provider.GetRequiredService<MongoConfiguration>();
                var settings = MongoClientSettings.FromConnectionString(config.ConnectionString);
                settings.ConnectTimeout = TimeSpan.FromMilliseconds(config.ConnectionTimeoutMs);
                return new MongoClient(settings);
            });

            // Регистрируем IMongoDatabase
            services.AddScoped<IMongoDatabase>(serviceProvider =>
            {
                var client = serviceProvider.GetRequiredService<IMongoClient>();
                return client.GetDatabase(mongoConfig.DatabaseName);
            });

            return services;
        }

        /// <summary>
        /// Подключение репозитория MongoDB
        /// </summary>
        /// <typeparam name="TDocument">Тип документа</typeparam>
        /// <param name="services">DI</param>
        /// <param name="collectionName">Название коллекции</param>
        /// <returns>DI</returns>
        public static IServiceCollection AddMongoRepository<TDocument>(
            this IServiceCollection services,
            string collectionName) where TDocument : IDocument
        {
            services.AddScoped<IMongoRepository<TDocument>>(provider =>
            {
                var dbService = provider.GetRequiredService<MongoDbService>();
                return new MongoRepository<TDocument>(dbService, collectionName);
            });

            return services;
        }
    }
}
