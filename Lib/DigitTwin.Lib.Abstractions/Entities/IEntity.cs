namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Интерфейс модели данных
    /// </summary>
    /// <typeparam name="TKeyType">Тип ИД</typeparam>
    public interface IEntity<TKeyType>
    {
        /// <summary>
        /// ИД
        /// </summary>
        TKeyType Id { get; set; }
    }
}
