using AutoMapper;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Users
{
    /// <inheritdoc cref="IUserService"/>
    internal class UserService : IUserService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
        private readonly IRepository<Guid, User> _repository;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        /// <inheritdoc cref="ILoggerService"/>
        private readonly ILoggerService _logger;

        public UserService(IRepository<Guid, User> repository, IMapper mapper, ILoggerService logger)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }
        #endregion

        public async Task<IBaseApiResponse> Create(UserCreateDto userCreateDto)
        {
            var response = new BaseApiResponse<UserDto>();

            var model = _mapper.Map<User>(userCreateDto);

            var result = await _repository.Create(model);

            if(result == null)
            {
                response.StatusCode = 400;
                response.Errors.Add("NotCreated", "Не удалось создать пользователя");
                return response;
            }

            response.StatusCode = 201;
            response.Body = _mapper.Map<UserDto>(result);

            return response;
        }

        public Task<IBaseApiResponse> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> GetAll(string email, string name, int type, int status)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> IsEmailExists(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> Update(UserDto userDto)
        {
            throw new NotImplementedException();
        }
    }
}
