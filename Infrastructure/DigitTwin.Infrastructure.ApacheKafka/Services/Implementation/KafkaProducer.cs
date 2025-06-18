using Confluent.Kafka;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <inheritdoc cref="IKafkaProducer{TKey, TValue}"/>
    internal class KafkaProducer<TKey, TValue> : IKafkaProducer<TKey, TValue>, IDisposable
    {
        /// <inheritdoc cref="IProducer{TKey, TValue}"/>
        private readonly IProducer<TKey, TValue> _producer;

        /// <summary>
        /// Топик
        /// </summary>
        private readonly string _topic;

        /// <summary>
        /// Создание продюсера на основе конфигурации
        /// </summary>
        public KafkaProducer(KafkaProducerConfig config)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = config.BootstrapServers
            };

            if (config.Properties != null)
            {
                foreach (var prop in config.Properties)
                {
                    producerConfig.Set(prop.Key, prop.Value);
                }
            }

            _producer = new ProducerBuilder<TKey, TValue>(producerConfig).Build();
            _topic = config.Topic;
        }

        /// <summary>
        /// Асинхронная отправка сообщения
        /// </summary>
        public async Task ProduceAsync(TKey key, TValue value)
        {
            var message = new Message<TKey, TValue> { Key = key, Value = value };
            await _producer.ProduceAsync(_topic, message);
        }

        /// <summary>
        /// Синхронная отправка сообщения
        /// </summary>
        public void Produce(TKey key, TValue value, Action<DeliveryReport<TKey, TValue>>? deliveryHandler = null)
        {
            var message = new Message<TKey, TValue> { Key = key, Value = value };
            _producer.Produce(_topic, message, deliveryHandler);
        }

        /// <summary>
        /// Освобождение ресурсов продюсера
        /// </summary>
        public void Dispose()
        {
            _producer.Flush(TimeSpan.FromSeconds(10));
            _producer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
