using DigitTwin.Lib.Abstractions.Services;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users
{
    /// <summary>
    /// Сервис по работе с пользователями
    /// </summary>
    public interface IUserService : IBaseCrudService<Guid, UserDto, UserCreateDto>
    {
        
    }
}
