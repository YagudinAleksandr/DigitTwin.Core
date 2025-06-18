namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Корневая конфигурация Kafka, содержащая словари продюсеров и консьюмеров
    /// </summary>
    public class KafkaConfiguration
    {
        /// <summary>
        /// Словарь конфигураций продюсеров (ключ - имя конфигурации)
        /// </summary>
        public Dictionary<string, KafkaProducerConfig> Producers { get; set; } = new();

        /// <summary>
        /// Словарь конфигураций консьюмеров (ключ - имя конфигурации)
        /// </summary>
        public Dictionary<string, KafkaConsumerConfig> Consumers { get; set; } = new();
    }
}
