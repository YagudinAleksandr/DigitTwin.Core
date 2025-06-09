using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис для работы с пользователями
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Существует ли пользователь с таким E-mail
        /// </summary>
        /// <param name="email">E-mail</param>
        /// <returns>true - существует, false - не существует</returns>
        Task<IBaseApiResponse> IsEmailExists(string email);
        Task<IBaseApiResponse> Create(UserCreateDto userCreateDto);
        Task<IBaseApiResponse> Delete(Guid id);
        Task<IBaseApiResponse> Update(UserDto userDto);
        Task<IBaseApiResponse> GetById(Guid id);
        Task<IBaseApiResponse> GetAll(string email, string name, int type, int status);
    }
}
