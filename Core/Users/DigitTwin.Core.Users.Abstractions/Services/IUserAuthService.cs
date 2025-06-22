using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис авторизации пользователей
    /// </summary>
    public interface IUserAuthService
    {
        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="userAuthRequestDto">Данные авторизации <see cref="UserAuthRequestDto"/></param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> Login(UserAuthRequestDto userAuthRequestDto);

        /// <summary>
        /// Выход из авторизации
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns><see cref="IBaseApiResponse"/></returns>
        Task<IBaseApiResponse> Logout(Guid userId);

        Task<IBaseApiResponse> Refresh(Guid userId);
    }
}
