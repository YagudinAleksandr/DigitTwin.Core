namespace DigitTwin.Infrastructure.Mongo
{
    /// <summary>
    /// Базовая модель хранения данных в MongoDB
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// ИД
        /// </summary>
        string Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime CreatedAt { set; get; }
    }
}
