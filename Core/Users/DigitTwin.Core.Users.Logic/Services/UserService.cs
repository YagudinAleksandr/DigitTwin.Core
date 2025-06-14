using AutoMapper;
using DigitTwin.Core.ActionService;
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

        /// <inheritdoc cref="IActionService"/>
        private readonly IActionService _actionService;

        public UserService(IRepository<Guid, User> repository, IMapper mapper, ILoggerService logger, IActionService actionService)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _actionService = actionService;
        }
        #endregion

        public async Task<IBaseApiResponse> Create(UserCreateDto userCreateDto)
        {
            var validationResult = userCreateDto.Run();

            if (!validationResult.IsValid)
            {
                var validationErrors = new Dictionary<string, string>();

                int validCount = 0;
                foreach (var error in validationResult.Errors)
                {
                    validationErrors.Add($"ValidationError{validCount}", error);
                }
                return _actionService.BadRequestResponse(validationErrors);
            }

            var model = _mapper.Map<User>(userCreateDto);

            var result = await _repository.Create(model);

            if (result == null)
            {
                var errors = new Dictionary<string, string>();
                errors.Add("CreateError", "Не удалось создать пользователя");

                return _actionService.BadRequestResponse(errors);
            }

            var createdUser = _mapper.Map<UserDto>(result);

            return _actionService.CreatedResponse(createdUser);
        }

        public async Task<IBaseApiResponse> Delete(Guid id)
        {
            var user = await _repository.GetByFilter(new GetSingleUserById(id));

            if (user == null)
            {
                return _actionService.NotFoundResponse($"Пользователь с ID {id} не найден");
            }

            await _repository.Delete(user);

            return _actionService.NoContentResponse();
        }

        public Task<IBaseApiResponse> GetAll(int maxElements, int startPosition, int endPosition)
        {
            // TODO: Подумать над фильтрацией
            throw new NotImplementedException();
        }

        public async Task<IBaseApiResponse> GetById(Guid id)
        {
            var user = await _repository.GetByFilter(new GetSingleUserById(id));

            if (user == null)
            {
                return _actionService.NotFoundResponse($"Пользователь с ID {id} не найден");
            }

            return _actionService.OkResponse(_mapper.Map<UserDto>(user));
        }

        public async Task<IBaseApiResponse> IsEmailExists(string email)
        {
            var user = await _repository.GetByFilter(new GetSingleUserByEmail(email));

            return user != null ? _actionService.OkResponse(true) : _actionService.OkResponse(false);
        }

        public async Task<IBaseApiResponse> Update(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            user = await _repository.Update(user);

            if (user == null)
            {
                var errors = new Dictionary<string, string>
                {
                    { "NotUpdated", "Не удалось обновить пользователя" }
                };

                return _actionService.BadRequestResponse(errors);
            }
            else
            {
                return _actionService.OkResponse(_mapper.Map<UserDto>(user));
            }
        }
    }
}
