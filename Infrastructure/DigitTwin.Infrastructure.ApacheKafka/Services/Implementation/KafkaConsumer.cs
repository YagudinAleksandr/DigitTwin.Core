using Confluent.Kafka;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <inheritdoc cref="IKafkaConsumer{TKey, TValue}"/>
    internal class KafkaConsumer<TKey, TValue> : IKafkaConsumer<TKey, TValue>, IDisposable
    {
        /// <inheritdoc cref="IConsumer{TKey, TValue}"/>
        private readonly IConsumer<TKey, TValue> _consumer;

        /// <summary>
        /// Создание консьюмера на основе конфигурации
        /// </summary>
        public KafkaConsumer(KafkaConsumerConfig config)
        {
            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.BootstrapServers,
                GroupId = config.GroupId,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            if (config.Properties != null)
            {
                foreach (var prop in config.Properties)
                {
                    consumerConfig.Set(prop.Key, prop.Value);
                }
            }

            _consumer = new ConsumerBuilder<TKey, TValue>(consumerConfig).Build();
            _consumer.Subscribe(config.Topic);
        }

        /// <summary>
        /// Чтение следующего сообщения
        /// </summary>
        public ConsumeResult<TKey, TValue> Consume(CancellationToken cancellationToken = default)
            => _consumer.Consume(cancellationToken);

        /// <summary>
        /// Фиксация обработки сообщения
        /// </summary>
        public void Commit(ConsumeResult<TKey, TValue> result)
            => _consumer.Commit(result);

        /// <summary>
        /// Освобождение ресурсов консьюмера
        /// </summary>
        public void Dispose()
        {
            _consumer.Close();
            _consumer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
