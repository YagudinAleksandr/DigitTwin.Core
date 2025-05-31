using DigitTwin.Lib.Abstractions.Filters;

namespace DigitTwin.Lib.Abstractions.Services
{
    /// <summary>
    /// Базовый сервис CRUD
    /// </summary>
    /// <typeparam name="TKey">Тип ИД</typeparam>
    /// <typeparam name="TEntityResponseDto">Тип ответа</typeparam>
    /// <typeparam name="TEntityRequestDto">Тип запроса</typeparam>
    public interface IBaseCrudService<TKey, TEntityDto, TEntityRequestDto>
    {
        /// <summary>
        /// Создание сущности
        /// </summary>
        /// <param name="entityRequest"><paramref name="entityRequest"/></param>
        /// <returns><paramref name="entityRequest"/></returns>
        Task<TEntityDto> Create(TEntityRequestDto entityRequest);

        /// <summary>
        /// Обновление сущности
        /// </summary>
        /// <param name="entityRequest"><paramref name="entityRequest"/></param>
        /// <returns><paramref name="entityRequest"/></returns>
        Task<TEntityDto> Update(TEntityDto entityRequest);

        /// <summary>
        /// Получение сущности по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns>
        /// <list type="bullet">
        /// <item>Существет ли объект</item>
        /// <item>Сущность</item>
        /// </list>
        /// </returns>
        Task<(bool isExist, TEntityDto entity)> GetById(TKey id);

        /// <summary>
        /// Удаление сущности
        /// </summary>
        /// <param name="id">ИД</param>
        Task Delete(TKey id);

        /// <summary>
        /// Получение списка сущностей
        /// </summary>
        /// <param name="baseFilter">Фильтр</param>
        /// <returns>Коллекция сущностей</returns>
        Task<IReadOnlyCollection<TEntityDto>> Get(IBaseFilter? baseFilter = null);
    }
}
