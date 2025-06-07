namespace DigitTwin.Lib.Abstractions.Services
{
    /// <summary>
    /// инициализатор базы данных
    /// </summary>
    public interface IDatabaseInitializer
    {
        /// <summary>
        /// Создание
        /// </summary>
        Task Up();
    }
}
