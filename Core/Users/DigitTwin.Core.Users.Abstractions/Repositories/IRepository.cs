using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Репозиторий для пользовательского сервиса
    /// </summary>
    /// <typeparam name="TKey">Тип ИД</typeparam>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
    {
        /// <summary>
        /// Получение сущности по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetByFilter(IBaseFilter<TEntity> filter);

        /// <summary>
        /// Получение коллекции по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Коллекция сущностей</returns>
        Task<IReadOnlyCollection<TEntity>> GetByFilterCollection(IBaseFilter<TEntity> filter);
    }
}
