namespace DigitTwin.Infrastructure.Redis
{
    /// <summary>
    /// Сервис для работы с данными Redis
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public interface IRedisService<T> where T : class
    {
        /// <summary>
        /// Получить запись
        /// </summary>
        /// <param name="key">ИД</param>
        /// <returns>Запись</returns>
        Task<T?> GetAsync(string key);

        /// <summary>
        /// Создать запись
        /// </summary>
        /// <param name="key">ИД</param>
        /// <param name="value">Значение</param>
        /// <param name="ttl">Время жизни запись</param>
        Task SetAsync(string key, T value, TimeSpan? ttl = null);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="key">ИД</param>
        /// <returns></returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// Существует ли запись
        /// </summary>
        /// <param name="key">ИД</param>
        /// <returns>true - существует, false - отсутствует</returns>
        Task<bool> ExistsAsync(string key);
    }
}
