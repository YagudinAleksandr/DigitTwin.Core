using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    /// <typeparam name="TKey">Тип ИД</typeparam>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IRepository<TKey, TEntity> : IBaseRepository<TKey, TEntity> where TEntity : class
    {
        /// <summary>
        /// Получение одной записи по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetSingleByFilter(IBaseFilter<TEntity> filter);

        /// <summary>
        /// Получение списка сущностей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список сущностей</returns>
        Task<IReadOnlyCollection<TEntity>> GetAllByFilter(IBaseFilter<TEntity> filter);
    }
}
