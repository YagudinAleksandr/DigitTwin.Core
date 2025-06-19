namespace DigitTwin.Lib.Abstractions
{
    /// <summary>
    /// Базовый интерфейс сервиса
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IBaseRepository<TKey, TEntity> where TEntity : class
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
        /// Получить список всех записе
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="maxPageSize">Максимальное количество элементов на странице</param>
        /// <param name="endPosition">Последний элемент</param>
        /// <param name="startPosition">Начальный элемент</param>
        /// <returns>
        /// <list type="number">
        /// <item>Список сущностей <typeparamref name="TEntity"/></item>
        /// <item>Общее число элементов</item>
        /// </list>
        /// </returns>
        Task<(IQueryable<TEntity> entities, int totalCount)> GetAll(Filter filter, int maxPageSize = 10, int startPosition = 1, int endPosition = 10);
    }
}
