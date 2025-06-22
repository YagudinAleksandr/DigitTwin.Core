using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
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
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseApiResponse<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<BaseBodyStub>))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<BaseBodyStub>))]
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
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<BaseBodyStub>))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<UserDto>))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<BaseBodyStub>))]
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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<bool>))]
    public async Task<IBaseApiResponse> IsEmailExists(string email)
    {
        return await _service.IsEmailExists(email);
    }

    /// <summary>
    /// Получение всех записей по фильтру
    /// </summary>
    /// <param name="filters">Фильтр</param>
    /// <param name="sortBy">Сортировка по полю</param>
    /// <param name="sortOrder">Направление сортировки</param>
    /// <param name="maxElements">Максимальное количество элементов</param>
    /// <param name="startPosition">Стартовый элемент</param>
    /// <param name="endPosition">Последний элемент</param>
    /// <remarks>GET /api/v1/users?filters[name]=Ivan&filters[status]=Active&sortBy=email&sortOrder=desc&maxElements=20&startPosition=1&endPosition=20</remarks>
    /// <returns></returns>
    [HttpGet(nameof(GetAll))]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(StatusCodes.Status206PartialContent, Type = typeof(BaseApiResponse<ItemCountDto<UserDto>>))]
    public async Task<IBaseApiResponse> GetAll([FromQuery] Dictionary<string, object> filters,
        [FromQuery] string? sortBy = null,
        [FromQuery] string? sortOrder = null,
        int maxElements = 10,
        int startPosition = 1,
        int endPosition = 10)
    {
        var filterRequest = new Filter
        {
            Filters = filters ?? [],
            SortBy = sortBy,
            SortOrder = sortOrder
        };

        return await _service.GetAllByFilter(filterRequest, maxElements, startPosition, endPosition);
    }
}