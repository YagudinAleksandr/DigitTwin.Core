namespace DigitTwin.Infrastructure.ApacheKafka
{
    /// <summary>
    /// Конфигурация консьюмера Kafka
    /// </summary>
    public class KafkaConsumerConfig
    {
        /// <summary>
        /// Адреса брокеров Kafka (разделенные запятыми)
        /// </summary>
        public string BootstrapServers { get; set; } = null!;

        /// <summary>
        /// Название топика для чтения сообщений
        /// </summary>
        public string Topic { get; set; } = null!;

        /// <summary>
        /// Идентификатор группы консьюмеров
        /// </summary>
        public string GroupId { get; set; } = null!;

        /// <summary>
        /// Дополнительные свойства конфигурации консьюмера
        /// </summary>
        public Dictionary<string, string>? Properties { get; set; }
    }
}
