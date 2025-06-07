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
    }
}
