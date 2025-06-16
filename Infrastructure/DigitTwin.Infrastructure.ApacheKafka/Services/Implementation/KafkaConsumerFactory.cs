using Microsoft.Extensions.Options;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <inheritdoc cref="IKafkaConsumerFactory"/>
    internal class KafkaConsumerFactory : IKafkaConsumerFactory
    {
        private readonly IOptions<KafkaConfiguration> _config;

        /// <summary>
        /// Инициализация фабрики с конфигурацией
        /// </summary>
        public KafkaConsumerFactory(IOptions<KafkaConfiguration> config)
        {
            _config = config;
        }

        /// <summary>
        /// Создание консьюмера по имени конфигурации
        /// </summary>
        public IKafkaConsumer<TKey, TValue> CreateConsumer<TKey, TValue>(string configName)
        {
            if (!_config.Value.Consumers.TryGetValue(configName, out var consumerConfig))
                throw new ArgumentException($"Конфигурация консьюмера '{configName}' не найдена");

            return new KafkaConsumer<TKey, TValue>(consumerConfig);
        }
    }
}
