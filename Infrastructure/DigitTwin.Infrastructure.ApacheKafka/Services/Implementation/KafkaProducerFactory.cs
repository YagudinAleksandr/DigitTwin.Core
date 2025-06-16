using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <inheritdoc cref="IKafkaProducerFactory"/>
    internal class KafkaProducerFactory : IKafkaProducerFactory, IDisposable
    {
        /// <inheritdoc cref="KafkaConfiguration"/>
        private readonly KafkaConfiguration _config;

        /// <summary>
        /// Список продюсеров
        /// </summary>
        private readonly ConcurrentDictionary<string, object> _producers = new();

        /// <summary>
        /// Объект уничтожен
        /// </summary>
        private bool _disposed;

        /// <summary>
        /// Инициализация фабрики с конфигурацией
        /// </summary>
        public KafkaProducerFactory(IOptions<KafkaConfiguration> config)
        {
            _config = config.Value;
        }

        /// <summary>
        /// Получение продюсера по имени конфигурации
        /// </summary>
        public IKafkaProducer<TKey, TValue> GetProducer<TKey, TValue>(string configName)
        {
            if (_disposed)
                throw new ObjectDisposedException("KafkaProducerFactory");

            var key = $"{configName}_{typeof(TKey).Name}_{typeof(TValue).Name}";
            return (IKafkaProducer<TKey, TValue>)_producers.GetOrAdd(key, _ =>
            {
                if (!_config.Producers.TryGetValue(configName, out var producerConfig))
                    throw new ArgumentException($"Конфигурация продюсера '{configName}' не найдена");

                return new KafkaProducer<TKey, TValue>(producerConfig);
            });
        }

        /// <summary>
        /// Освобождение всех созданных продюсеров
        /// </summary>
        public void Dispose()
        {
            if (_disposed) return;

            foreach (var producer in _producers.Values.OfType<IDisposable>())
            {
                producer.Dispose();
            }
            _producers.Clear();
            _disposed = true;
        }
    }
}
