namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Фабрика для создания экземпляров консьюмеров Kafka
    /// </summary>
    public interface IKafkaConsumerFactory
    {
        /// <summary>
        /// Создание консьюмера по имени конфигурации
        /// </summary>
        /// <param name="configName">Имя конфигурации из appsettings.json</param>
        IKafkaConsumer<TKey, TValue> CreateConsumer<TKey, TValue>(string configName);
    }
}
