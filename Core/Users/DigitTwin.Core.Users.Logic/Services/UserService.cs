using DigitTwin.Lib.Abstractions.Filters;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users.Logic.Services
{
    internal class UserService : IUserService
    {
        public Task<UserDto> Create(UserCreateDto entityRequest)
        {
            throw new NotImplementedException();
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<UserDto>> Get(IBaseFilter? baseFilter = null)
        {
            throw new NotImplementedException();
        }

        public Task<(bool isExist, UserDto entity)> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDto> Update(UserDto entityRequest)
        {
            throw new NotImplementedException();
        }
    }
}
