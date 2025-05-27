using DigitTwin.Lib.Abstractions.Services;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users
{
    public interface IUserService : IBaseCrudService<Guid, UserDto, UserCreateDto>
    {
        
    }
}
