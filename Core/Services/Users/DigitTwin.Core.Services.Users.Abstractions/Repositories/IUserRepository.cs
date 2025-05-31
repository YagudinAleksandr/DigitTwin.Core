using DigitTwin.Lib.Abstractions;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Репозиторий пользователей
    /// </summary>
    /// <typeparam name="TKey">Тип ключа</typeparam>
    /// <typeparam name="TEntity">Тип сущности</typeparam>
    public interface IUserRepository<TKey, TEntity> : IBaseService<TKey, TEntity> where TEntity : IEntity<TKey>
    {
    }
}
