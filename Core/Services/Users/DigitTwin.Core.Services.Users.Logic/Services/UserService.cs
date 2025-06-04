using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Services.Users.Logic.Services
{
    /// <inheritdoc cref="IUserService"/>
    internal class UserService : IUserService
    {
        #region CTOR
        /// <inheritdoc cref="IUserRepository{TKey, TEntity}"/>
        private readonly IUserRepository<Guid, User> _userRepository;

        /// <inheritdoc cref="ILoggerService"/>
        private readonly ILoggerService _loggerService;

        public string ServiceName => nameof(UserService);
        public UserService(IUserRepository<Guid, User> userRepository, ILoggerService loggerService)
        {
            _userRepository = userRepository;
            _loggerService = loggerService;
        }
        #endregion

        public Task<BaseApiResponse<UserDto>> Create(UserCreateDto user)
        {
            throw new NotImplementedException();
        }

        public Task<BaseApiResponse<bool>> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseApiResponse<UserDto>> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<BaseApiResponse<IReadOnlyCollection<UserDto>>> GetAll(GetSingleUserFilter<UserDto> filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseApiResponse<UserDto>> GetByFilter(GetSingleUserFilter<UserDto> filter)
        {
            throw new NotImplementedException();
        }

        public Task<BaseApiResponse<UserDto>> Update(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
