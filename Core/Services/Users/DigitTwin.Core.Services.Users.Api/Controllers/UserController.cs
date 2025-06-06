using DigitTwin.Lib.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Services.Users.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost(nameof(Create))]
        public async Task<IActionResult> Create(UserCreateDto user)
        {
            var action = await _userService.Create(user);

            return Ok(action);
        }
    }
}
