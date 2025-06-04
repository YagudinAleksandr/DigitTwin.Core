namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Базовый интерфейс сервиса
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IBaseService<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entity">Сущность <typeparamref name="TEntity"/></param>
        /// <returns>Сущность <typeparamref name="TEntity"/></returns>
        Task<TEntity?> Create(TEntity entity);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entity">Сущность <typeparamref name="TEntity"/></param>
        /// <returns>Сущность <typeparamref name="TEntity"/></returns>
        Task<TEntity?> Update(TEntity entity);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="entity">Сущность <typeparamref name="TEntity"/></param>
        Task Delete(TEntity entity);

        /// <summary>
        /// Получение по ИД
        /// </summary>
        /// <param name="key">ИД <typeparamref name="TKey"/></param>
        /// <returns>Сущность <typeparamref name="TEntity"/></returns>
        Task<TEntity?> GetById(TKey key);
    }
}
