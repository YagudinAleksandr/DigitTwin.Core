using AutoMapper;
using DigitTwin.Core.ActionService;
using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Translations.Translators;
using FluentValidation;

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

        /// <inheritdoc cref="IActionService"/>
        private readonly IActionService _actionService;

        /// <inheritdoc cref="IValidator"/>
        private readonly IValidator<UserCreateDto> _validator;

        public UserService(IRepository<Guid, User> repository,
            IMapper mapper,
            IActionService actionService,
            IValidator<UserCreateDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _actionService = actionService;
            _validator = validator;
        }
        #endregion

        public async Task<IBaseApiResponse> Create(UserCreateDto userCreateDto)
        {
            var user = await _repository.GetByFilter(new GetSingleUserByEmail(userCreateDto.Email));

            if (user != null)
            {
                var errors = new List<string>()
                {
                    Errors.AlreadyExist(Entities.User())
                };
                return _actionService.BadRequestResponse(errors);
            }

            var validationContext = new ValidationContext<UserCreateDto>(userCreateDto);
            var validationResult = await _validator.ValidateAsync(validationContext);

            if (!validationResult.IsValid)
            {
                var errors = new List<string>();
                foreach (var error in validationResult.Errors)
                {
                    errors.Add(error.ErrorMessage);
                }
                return _actionService.BadRequestResponse(errors);
            }

            var model = _mapper.Map<User>(userCreateDto);

            var result = await _repository.Create(model);

            if (result == null)
            {
                var errors = new List<string>
                {
                    Errors.CannotCreate(Entities.User())
                };

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
                return _actionService.NotFoundResponse(Errors.CannotFind(Entities.User(), "ID", $"{id}"));
            }

            await _repository.Delete(user);

            return _actionService.NoContentResponse();
        }

        public async Task<IBaseApiResponse> GetAllByFilter(Filter filter, int maxElements, int startPosition, int endPosition)
        {
            var (entities, totalCount) = await _repository.GetAll(filter, maxElements, startPosition, endPosition);

            return _actionService.PartialResponse(entities.ToList(), startPosition, endPosition, totalCount);
        }

        public async Task<IBaseApiResponse> GetById(Guid id)
        {
            var user = await _repository.GetByFilter(new GetSingleUserById(id));

            if (user == null)
            {
                return _actionService.NotFoundResponse(Errors.CannotFind(Entities.User(), "ID", $"{id}"));
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
                var errors = new List<string>
                {
                    Errors.CannotUpdate(Entities.User())
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
