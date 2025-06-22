using AutoMapper;
using DigitTwin.Core.ActionService;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Misc.Tools;
using DigitTwin.Lib.Translations;

namespace DigitTwin.Core.Users
{
    /// <inheritdoc cref="IUserAuthService"/>
    internal class UserAuthService : IUserAuthService
    {
        #region CTOR
        /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
        private readonly IRepository<Guid, User> _repository;

        /// <inheritdoc cref="IActionService"/>
        private readonly IActionService _actionService;

        /// <inheritdoc cref="ITokenService"/>
        private readonly ITokenService _tokenService;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        public UserAuthService(IRepository<Guid, User> repository,
            ITokenService tokenService,
            IActionService actionService,
            IMapper mapper)
        {
            _repository = repository;
            _tokenService = tokenService;
            _actionService = actionService;
            _mapper = mapper;
        }
        #endregion

        public async Task<IBaseApiResponse> Login(UserAuthRequestDto userAuthRequestDto)
        {
            var user = await _repository.GetByFilter(new GetSingleUserByEmail(userAuthRequestDto.Email));

            if (user == null) 
            {
                var errors = new List<string>
                {
                    Errors.CannotFind(Entities.User(), Fields.Email(), userAuthRequestDto.Email)
                };

                return _actionService.BadRequestResponse(errors);
            }

            if(user.PasswordSalt == null || user.Password == null)
            {
                var errors = new List<string>
                {
                    Errors.CannotFind(Entities.User(), Fields.Email(), userAuthRequestDto.Email)
                };
                return _actionService.BadRequestResponse(errors);
            }

            if (!PasswordHasherTool.VerifyPasswordHash(userAuthRequestDto.Password, user.Password, user.PasswordSalt))
            {
                var errors = new List<string>
                {
                    Errors.CannotFind(Entities.User(), Fields.Email(), userAuthRequestDto.Email)
                };
                return _actionService.BadRequestResponse(errors);
            }

            var token = await _tokenService.CreateToken(user.Id, user.Email, user.Type, TokenTypeEnum.Auth);
            var refresh = await _tokenService.CreateToken(user.Id, user.Email, user.Type, TokenTypeEnum.Refresh);

            var userResponse = new UserAuthResponseDto()
            {
                AuthToken = token,
                RefreshToken = refresh,
                User = _mapper.Map<UserDto>(user)
            };

            return _actionService.OkResponse(userResponse);
        }

        public async Task<IBaseApiResponse> Logout(Guid userId)
        {
            await _tokenService.RemoveToken(userId,TokenTypeEnum.Auth);
            await _tokenService.RemoveToken(userId, TokenTypeEnum.Refresh);

            return _actionService.NoContentResponse();
        }

        public Task<IBaseApiResponse> Refresh(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
