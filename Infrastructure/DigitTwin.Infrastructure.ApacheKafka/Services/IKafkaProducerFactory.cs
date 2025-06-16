namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Фабрика для создания экземпляров продюсеров Kafka
    /// </summary>
    public interface IKafkaProducerFactory
    {
        /// <summary>
        /// Получение продюсера по имени конфигурации
        /// </summary>
        /// <param name="configName">Имя конфигурации из appsettings.json</param>
        IKafkaProducer<TKey, TValue> GetProducer<TKey, TValue>(string configName);
    }
}
