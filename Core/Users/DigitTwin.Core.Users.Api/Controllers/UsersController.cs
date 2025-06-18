using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Contracts.User;
using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Users.Api.Controllers;

/// <summary>
/// Контроллер для управления пользователями
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class UsersController : ControllerBase
{
    #region CTOR
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }
    #endregion

    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    [HttpPost(nameof(Create))]
    [MapToApiVersion("1.0")]
    public async Task<IBaseApiResponse> Create([FromBody] UserCreateDto user)
    {
        return await _service.Create(user);
    }

    /// <summary>
    /// Обновление пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    [HttpPost(nameof(Update))]
    [MapToApiVersion("1.0")]
    public async Task<IBaseApiResponse> Update([FromBody] UserDto user)
    {
        return await _service.Update(user);
    }

    /// <summary>
    /// Удаление пользователя
    /// </summary>
    /// <param name="id">ИД</param>
    [HttpDelete(nameof(Delete))]
    [MapToApiVersion("1.0")]
    public async Task<IBaseApiResponse> Delete(Guid id)
    {
        return await _service.Delete(id);
    }

    /// <summary>
    /// Получение пользователя по ИД
    /// </summary>
    /// <param name="id">ИД</param>
    [HttpGet(nameof(Get))]
    [MapToApiVersion("1.0")]
    public async Task<IBaseApiResponse> Get(Guid id)
    {
        return await _service.GetById(id);
    }

    /// <summary>
    /// Существует ли E-mail
    /// </summary>
    /// <param name="email">E-mail</param>
    [HttpGet(nameof(IsEmailExists))]
    [MapToApiVersion("1.0")]
    public async Task<IBaseApiResponse> IsEmailExists(string email)
    {
        return await _service.IsEmailExists(email);
    }
}