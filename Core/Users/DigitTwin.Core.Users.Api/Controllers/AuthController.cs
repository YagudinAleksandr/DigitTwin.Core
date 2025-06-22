using DigitTwin.Lib.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Users.Api.Controllers
{
    /// <summary>
    /// Контроллер управления авторизацией
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        #region CTOR
        /// <inheritdoc cref="IUserAuthService"/>
        private readonly IUserAuthService _service;
        public AuthController(IUserAuthService service)
        {
            _service = service;
        }
        #endregion

        /// <summary>
        /// Создание организации
        /// </summary>
        /// <param name="user">Организация</param>
        [HttpPost(nameof(Login))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserAuthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Login([FromBody] UserAuthRequestDto login)
        {
            return await _service.Login(login);
        }

        /// <summary>
        /// Выход из сессии
        /// </summary>
        /// <param name="userId">ИД пользователя</param>
        [HttpGet(nameof(Logout))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IBaseApiResponse> Logout(Guid userId)
        {
            return await _service.Logout(userId);
        }
    }
}
