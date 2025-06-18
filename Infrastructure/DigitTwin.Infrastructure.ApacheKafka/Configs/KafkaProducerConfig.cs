namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Конфигурация продюсера Kafka
    /// </summary>
    public class KafkaProducerConfig
    {
        /// <summary>
        /// Адреса брокеров Kafka (разделенные запятыми)
        /// </summary>
        public string BootstrapServers { get; set; } = null!;

        /// <summary>
        /// Название топика для отправки сообщений
        /// </summary>
        public string Topic { get; set; } = null!;

        /// <summary>
        /// Дополнительные свойства конфигурации продюсера
        /// </summary>
        public Dictionary<string, string>? Properties { get; set; }
    }
}
