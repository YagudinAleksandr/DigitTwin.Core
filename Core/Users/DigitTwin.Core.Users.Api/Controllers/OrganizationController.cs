using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace DigitTwin.Core.Users.Api.Controllers
{
    /// <summary>
    /// Контроллер для управления организациями
    /// </summary>
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class OrganizationController : ControllerBase
    {
        #region CTOR

        /// <inheritdoc cref="IOrganizationService"/>
        private readonly IOrganizationService _organizationService;

        public OrganizationController(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }

        #endregion

        /// <summary>
        /// Создание организации
        /// </summary>
        /// <param name="user">Организация</param>
        [HttpPost(nameof(Create))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BaseApiResponse<OrganizationDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Create([FromBody] OrganizationCreateRequestDto organization)
        {
            return await _organizationService.Create(organization);
        }

        /// <summary>
        /// Обновление организации
        /// </summary>
        /// <param name="user">Организация</param>
        [HttpPost(nameof(Update))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<OrganizationDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Update([FromBody] OrganizationDto organization)
        {
            return await _organizationService.Update(organization);
        }

        /// <summary>
        /// Удаление организации
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpDelete(nameof(Delete))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Delete(Guid id)
        {
            return await _organizationService.Delete(id);
        }

        /// <summary>
        /// Получение организации по ИД
        /// </summary>
        /// <param name="id">ИД</param>
        [HttpGet(nameof(Get))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<OrganizationDto>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BaseApiResponse<BaseBodyStub>))]
        public async Task<IBaseApiResponse> Get(Guid id)
        {
            return await _organizationService.GetById(id);
        }

        /// <summary>
        /// Существует ли организация
        /// </summary>
        /// <param name="inn">ИНН</param>
        [HttpGet(nameof(IsOrganizationExists))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BaseApiResponse<bool>))]
        public async Task<IBaseApiResponse> IsOrganizationExists(string inn)
        {
            return await _organizationService.IsOrganizationExists(inn);
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
        [HttpGet(nameof(GetAll))]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status206PartialContent, Type = typeof(BaseApiResponse<ItemCountDto<OrganizationDto>>))]
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

            return await _organizationService.GetAllByFilter(filterRequest, maxElements, startPosition, endPosition);
        }
    }
}
