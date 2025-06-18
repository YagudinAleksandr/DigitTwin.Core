using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Расширения для регистрации Kafka в DI-контейнере
    /// </summary>
    public static class KafkaServiceExtensions
    {
        /// <summary>
        /// Добавление сервисов Kafka в DI-контейнер
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация приложения</param>
        /// <param name="configSection">Секция конфигурации Kafka (по умолчанию "Kafka")</param>
        public static IServiceCollection AddKafka(
            this IServiceCollection services,
            IConfiguration configuration,
            string configSection = "Kafka")
        {
            // Регистрация конфигурации
            services.Configure<KafkaConfiguration>(_ => configuration.GetSection(configSection));

            // Регистрация фабрик
            services.AddSingleton<IKafkaProducerFactory, KafkaProducerFactory>();
            services.AddSingleton<IKafkaConsumerFactory, KafkaConsumerFactory>();

            return services;
        }
    }
}
