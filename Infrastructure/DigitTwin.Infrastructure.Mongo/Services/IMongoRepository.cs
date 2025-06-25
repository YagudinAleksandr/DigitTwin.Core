using System.Linq.Expressions;
using MongoDB.Driver;

namespace DigitTwin.Infrastructure.Mongo
{
    public interface IMongoRepository<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Внесение записи в MongoDB
        /// </summary>
        /// <param name="document">Документ <see cref="IDocument"/></param>
        Task InsertOneAsync(TDocument document);

        /// <summary>
        /// Получить документ по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>Документ</returns>
        Task<TDocument?> FindByIdAsync(string id);

        /// <summary>
        /// Обновить запись
        /// </summary>
        /// <param name="document">Документ</param>
        Task UpdateOneAsync(TDocument document);

        /// <summary>
        /// Удалить запись
        /// </summary>
        /// <param name="id">ИД</param>
        Task DeleteOneAsync(string id);

        /// <summary>
        /// Расширенный поиск по базе
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sort">Сортировка</param>
        /// <param name="limit">Ограничения</param>
        /// <returns>Коллекция</returns>
        Task<IEnumerable<TDocument>> FindAsync(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument>? sort = null,
            int? limit = null);

        /// <summary>
        /// Получение проекций
        /// </summary>
        /// <typeparam name="TProjection">Проекция</typeparam>
        /// <param name="filter">Фильтр</param>
        /// <param name="projection">Проекция</param>
        /// <param name="sort">Сортировка</param>
        /// <returns>Проекции</returns>
        Task<IEnumerable<TProjection>> ProjectAsync<TProjection>(
            Expression<Func<TDocument, bool>> filter,
            Expression<Func<TDocument, TProjection>> projection,
            SortDefinition<TDocument>? sort = null);

        /// <summary>
        /// Количество элементов
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Количество элементов</returns>
        Task<long> CountAsync(Expression<Func<TDocument, bool>> filter);

        /// <summary>
        /// Проверка на наличее записи
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>true - существет, false - отсутствует</returns>
        Task<bool> ExistsAsync(Expression<Func<TDocument, bool>> filter);

        /// <summary>
        /// Аггрегация
        /// </summary>
        /// <returns>Аггрегация</returns>
        IAggregateFluent<TDocument> Aggregate();

        /// <summary>
        /// Пагинация
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <param name="sort">Сортировка</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Количество элементов на странице</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Записи</item>
        /// <item>Количество</item>
        /// </list>
        /// </returns>
        Task<(IEnumerable<TDocument> items, long totalCount)> PaginateAsync(
            Expression<Func<TDocument, bool>> filter,
            SortDefinition<TDocument> sort,
            int pageNumber,
            int pageSize);
    }
}
