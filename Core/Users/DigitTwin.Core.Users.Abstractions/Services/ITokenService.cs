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
        /// <param name="tokenType">Тип токена</param>
        /// <returns>Созданный JWT токен</returns>
        Task<TokenInfoDto> CreateToken(Guid userId, string email, UserTypeEnum userType, TokenTypeEnum tokenType);

        /// <summary>
        /// Сохранить токен в Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="token">JWT токен</param>
        /// <param name="expirationTime">Время истечения токена</param>
        /// <param name="tokenType">Тип токена</param>
        Task SaveToken(Guid userId, string token, TimeSpan expirationTime, TokenTypeEnum tokenType);

        /// <summary>
        /// Получить токен из Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="tokenType">Тип токена</param>
        /// <returns>Токен или null если не найден</returns>
        Task<string?> GetToken(Guid userId, TokenTypeEnum tokenType);

        /// <summary>
        /// Удалить токен из Redis
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="tokenType">Тип токена</param>
        Task RemoveToken(Guid userId, TokenTypeEnum tokenType);

        /// <summary>
        /// Проверить валидность JWT токена
        /// </summary>
        /// <param name="token">JWT токен для проверки</param>
        /// <param name="tokenType">Тип токена</param>
        /// <returns>true если токен валиден, false в противном случае</returns>
        Task<bool> ValidateToken(string token, TokenTypeEnum tokenType);

        /// <summary>
        /// Получить информацию из JWT токена
        /// </summary>
        /// <param name="token">JWT токен</param>
        /// <param name="tokenType">Тип токена</param>
        /// <returns>Информация о токене или null</returns>
        Task<TokenInfoDto?> GetTokenInfo(string token, TokenTypeEnum tokenType);

        /// <summary>
        /// Обновить JWT токен
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="userType">Тип пользователя</param>
        /// <returns>Новый JWT токен</returns>
        Task<TokenInfoDto> RefreshToken(Guid userId, string email, UserTypeEnum userType, TokenTypeEnum tokenType);
    }
}
