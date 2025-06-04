using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Services.Users
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Название сервиса
        /// </summary>
        public string ServiceName {  get; }
        /// <summary>
        /// Создание пользователя
        /// </summary>
        /// <param name="user">Пользователь <see cref="UserDto"/></param>
        /// <returns>Пользователь</returns>
        Task<BaseApiResponse<UserDto>> Create(UserCreateDto user);

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="user">Пользователь <see cref="UserDto"/></param>
        /// <returns>Пользователь</returns>
        Task<BaseApiResponse<UserDto>> Update(UserDto user);

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="id">ИД пользователь</param>
        /// <returns>true - удален, false - не удалось удалить</returns>
        Task<BaseApiResponse<bool>> Delete(Guid id);

        /// <summary>
        /// Получение пользователя по ИД
        /// </summary>
        /// <param name="id">ИД пользователя</param>
        /// <returns>Пользователь</returns>
        Task<BaseApiResponse<UserDto>> Get(Guid id);

        /// <summary>
        /// Получение пользователя по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Пользователь</returns>
        Task<BaseApiResponse<UserDto>> GetByFilter(GetSingleUserFilter<UserDto> filter);

        /// <summary>
        /// Получение списка пользователей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Коллекция пользователей</returns>
        Task<BaseApiResponse<IReadOnlyCollection<UserDto>>> GetAll(GetSingleUserFilter<UserDto> filter);
    }
}
