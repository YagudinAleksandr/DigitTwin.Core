using AutoMapper;
using DigitTwin.Core.ActionService;
using DigitTwin.Infrastructure.LoggerSeq;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;

namespace DigitTwin.Core.Services.Users
{
    /// <inheritdoc cref="IUserService"/>
    internal class UserService : IUserService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
        private readonly IRepository<Guid, User> _userRepository;

        /// <inheritdoc cref="ILoggerService"/>
        //private readonly ILoggerService _loggerService;

        /// <inheritdoc cref="IActionResponse"/>
        private readonly IActionResponse _actionResponse;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public string ServiceName => nameof(UserService);
        public UserService(IRepository<Guid, User> userRepository,
            //ILoggerService loggerService,
            IActionResponse actionResponse,
            IMapper mapper)
        {
            _userRepository = userRepository;
            //_loggerService = loggerService;
            _actionResponse = actionResponse;
            _mapper = mapper;
        }

        #endregion

        public async Task<IBaseApiResponse> Create(UserCreateDto user)
        {
            var validationResult = user.Run();

            if (validationResult != null && !validationResult.IsValid)
            {
                return _actionResponse.BadRequestResponse(new Dictionary<string, string>
                {
                    {"ValidationErrors",validationResult.Errors.ToString()! }
                });
            }

            var model = _mapper.Map<User>(user);
            model = await _userRepository.Create(model);

            if (model == null)
            {
                return _actionResponse.BadRequestResponse(new Dictionary<string, string>
                {
                    { "CanNotCreate","Не удалось создать пользователя" }
                });
            }

            return _actionResponse.CreatedResponse(_mapper.Map<UserDto>(model));
        }

        public async Task<IBaseApiResponse> Update(UserDto user)
        {
            var validationResult = user.Run();

            if (validationResult != null && !validationResult.IsValid)
            {
                return _actionResponse.BadRequestResponse(new Dictionary<string, string>
                {
                    {"ValidationErrors",validationResult.Errors.ToString()! }
                });
            }

            var model = _mapper.Map<User>(user);
            model = await _userRepository.Update(model);

            if (model == null)
            {
                return _actionResponse.BadRequestResponse(new Dictionary<string, string>
                {
                    { "CanNotUpdate","Не удалось обновить пользователя" }
                });
            }

            return _actionResponse.CreatedResponse(_mapper.Map<UserDto>(model));
        }

        public async Task<IBaseApiResponse> Delete(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if(user == null)
            {
                return _actionResponse.NotFoundResponse($"Пользователь с ИД {id} не найден для удаления");
            }

            await _userRepository.Delete(user);

            return _actionResponse.NoContentResponse();
        }

        public async Task<IBaseApiResponse> Get(Guid id)
        {
            var user = await _userRepository.GetById(id);

            if (user == null)
            {
                return _actionResponse.NotFoundResponse($"Пользователь с ИД {id} не найден");
            }

            var userDto = _mapper.Map<UserDto>(user);

            return _actionResponse.OkResponse(userDto);
        }

        public Task<IBaseApiResponse> GetByFilter(GetSingleUserFilter<UserDto> filter)
        {
            //var filterResponse = await _userRepository.GetSingleByFilter(filter);

            //if (filterResponse == null)
            //{
            //    return _actionResponse.NotFoundResponse("Пользователь не найден");
            //}

            //var dto = _mapper.Map<UserDto>(filterResponse);

            //return _actionResponse.OkResponse(dto);
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> GetAll(GetSingleUserFilter<UserDto> filter)
        {
            throw new NotImplementedException();
        }
    }
}
