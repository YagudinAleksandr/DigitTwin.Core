using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Users.Api.Controllers
{
    /// <summary>
    /// Контроллер авторизации пользователя
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UserAuthController : ControllerBase
    {
        /// <summary>
        /// Запрос авторизации пользователя
        /// </summary>
        /// <param name="authRequest">Запрос авторизации <see cref="UserAuthRequestDto"/></param>
        [HttpPost(nameof(Auth))]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserAuthResponseDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type=typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Auth([FromBody] UserAuthRequestDto authRequest)
        {
            throw new NotImplementedException();
        }
    }
}
