using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Users.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
public class UsersController : ControllerBase
{
    [HttpGet]
    [MapToApiVersion("1.0")]
    public IActionResult GetUsersV1()
    {
        return Ok(new { message = "This is version 1.0 of the API" });
    }

    [HttpGet]
    [MapToApiVersion("2.0")]
    public IActionResult GetUsersV2()
    {
        return Ok(new { message = "This is version 2.0 of the API with enhanced features" });
    }

    // Этот метод будет доступен в обеих версиях API
    [HttpGet("{id}")]
    public IActionResult GetUserById(int id)
    {
        return Ok(new { id, message = "This endpoint is available in all versions" });
    }
} 