using Confluent.Kafka;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Интерфейс консьюмера Kafka для чтения сообщений
    /// </summary>
    /// <typeparam name="TKey">Тип ключа сообщения</typeparam>
    /// <typeparam name="TValue">Тип значения сообщения</typeparam>
    public interface IKafkaConsumer<TKey, TValue>
    {
        /// <summary>
        /// Чтение следующего сообщения
        /// </summary>
        ConsumeResult<TKey, TValue> Consume(CancellationToken cancellationToken = default);

        /// <summary>
        /// Фиксация обработки сообщения
        /// </summary>
        void Commit(ConsumeResult<TKey, TValue> result);
    }
}
