using Confluent.Kafka;

namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Интерфейс продюсера Kafka для отправки сообщений
    /// </summary>
    /// <typeparam name="TKey">Тип ключа сообщения</typeparam>
    /// <typeparam name="TValue">Тип значения сообщения</typeparam>
    public interface IKafkaProducer<TKey, TValue>
    {
        /// <summary>
        /// Асинхронная отправка сообщения
        /// </summary>
        Task ProduceAsync(TKey key, TValue value);

        /// <summary>
        /// Синхронная отправка сообщения с обработчиком доставки
        /// </summary>
        void Produce(TKey key, TValue value, Action<DeliveryReport<TKey, TValue>>? deliveryHandler = null);
    }
}
