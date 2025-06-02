using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    /// <typeparam name="TKey">Тип ИД</typeparam>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public interface IUserRepository<TKey, TEntity> : IBaseService<TKey, TEntity> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// Получение одной записи по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Сущность</returns>
        Task<TEntity?> GetSingleByFilter(GetSingleUserFilter<TEntity> filter);

        /// <summary>
        /// Получение списка сущностей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список сущностей</returns>
        Task<IReadOnlyCollection<TEntity>> GetAllByFilter(GetSingleUserFilter<TEntity> filter);
    }
}
