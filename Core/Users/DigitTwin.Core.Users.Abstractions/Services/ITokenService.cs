using DigitTwin.Lib.Contracts;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис для работы с JWT токенами
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Создать JWT токен для пользователя
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <returns>Созданный JWT токен</returns>
        Task<TokenInfoDto> CreateTokenAsync(Guid userId, string email, int userType);

        /// <summary>
        /// Сохранить токен в Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="token">JWT токен</param>
        /// <param name="expirationTime">Время истечения токена</param>
        /// <returns>Task</returns>
        Task SaveTokenAsync(Guid userId, string token, TimeSpan expirationTime);

        /// <summary>
        /// Получить токен из Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Токен или null если не найден</returns>
        Task<string?> GetTokenAsync(Guid userId);

        /// <summary>
        /// Удалить токен из Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <returns>Task</returns>
        Task RemoveTokenAsync(Guid userId);

        /// <summary>
        /// Проверить валидность JWT токена
        /// </summary>
        /// <param name="token">JWT токен для проверки</param>
        /// <returns>true если токен валиден, false в противном случае</returns>
        Task<bool> ValidateTokenAsync(string token);

        /// <summary>
        /// Получить информацию из JWT токена
        /// </summary>
        /// <param name="token">JWT токен</param>
        /// <returns>Информация о токене или null</returns>
        Task<TokenInfoDto?> GetTokenInfoAsync(string token);

        /// <summary>
        /// Обновить JWT токен
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <returns>Новый JWT токен</returns>
        Task<TokenInfoDto> RefreshTokenAsync(Guid userId, string email, int userType);
    }
}
