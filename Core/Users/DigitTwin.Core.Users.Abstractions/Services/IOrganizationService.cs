using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис для работы с организациями
    /// </summary>
    public interface IOrganizationService
    {
        /// <summary>
        /// Существует ли организация
        /// </summary>
        /// <param name="inn">ИНН</param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> IsOrganizationExists(string inn);

        /// <summary>
        /// Создание организации
        /// </summary>
        /// <param name="organization">Организация <see cref="OrganizationCreateRequestDto"/></param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> Create(OrganizationCreateRequestDto organization);

        /// <summary>
        /// Обновление организации
        /// </summary>
        /// <param name="organization">Организация <see cref="OrganizationDto"/></param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> Update(OrganizationDto organization);

        /// <summary>
        /// Получение организации по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> GetById(Guid id);

        /// <summary>
        /// Удаление организации
        /// </summary>
        /// <param name="id">ИД</param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> Delete(Guid id);

        /// <summary>
        /// Получение списка всех организаций по фильтру
        /// </summary>
        /// <param name="filter">Фильтр <see cref="Filter"/></param>
        /// <param name="maxElements">Максимальное количество записей для показа</param>
        /// <param name="startPosition">Стартовая позиция</param>
        /// <param name="endPosition">Конечная позиция</param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> GetAllByFilter(Filter filter, int maxElements, int startPosition, int endPosition);
    }
}
