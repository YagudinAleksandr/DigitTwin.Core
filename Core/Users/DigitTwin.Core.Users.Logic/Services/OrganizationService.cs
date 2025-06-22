using AutoMapper;
using DigitTwin.Core.ActionService;
using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
using DigitTwin.Lib.Translations;
using FluentValidation;

namespace DigitTwin.Core.Users
{
    /// <inheritdoc cref="IOrganizationService"/>
    internal class OrganizationService : IOrganizationService
    {
        #region CTOR

        /// <inheritdoc cref="IRepository{TKey, TEntity}"/>
        private readonly IRepository<Guid, Organization> _repository;

        /// <inheritdoc cref="IMapper"/>
        private readonly IMapper _mapper;

        /// <inheritdoc cref="IActionService"/>
        private readonly IActionService _actionService;

        // <inheritdoc cref="IValidator"/>
        private readonly IValidator<OrganizationCreateRequestDto> _validator;

        public OrganizationService(IRepository<Guid, Organization> repository,
            IMapper mapper,
            IActionService actionService,
            IValidator<OrganizationCreateRequestDto> validator)
        {
            _actionService = actionService;
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        #endregion

        public async Task<IBaseApiResponse> Create(OrganizationCreateRequestDto organization)
        {
            var existingOrg = await _repository.GetByFilter(new GetSingleOrganizationByInn(organization.Inn));

            if (existingOrg != null)
            {
                var errors = new List<string>()
                {
                    Errors.AlreadyExist(Entities.Organization())
                };

                return _actionService.BadRequestResponse(errors);
            }

            var validationContext = new ValidationContext<OrganizationCreateRequestDto>(organization);
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

            var model = _mapper.Map<Organization>(organization);

            var result = await _repository.Create(model);

            if (result == null)
            {
                var errors = new List<string>
                {
                    Errors.CannotCreate(Entities.Organization())
                };

                return _actionService.BadRequestResponse(errors);
            }

            var createdOrganization = _mapper.Map<OrganizationDto>(result);

            return _actionService.CreatedResponse(createdOrganization);
        }

        public async Task<IBaseApiResponse> Delete(Guid id)
        {
            var user = await _repository.GetByFilter(new GetSingleOrganizationById(id));

            if (user == null)
            {
                return _actionService.NotFoundResponse(Errors.CannotFind(Entities.Organization(), "ID", $"{id}"));
            }

            await _repository.Delete(user);

            return _actionService.NoContentResponse();
        }

        public async Task<IBaseApiResponse> GetAllByFilter(Filter filter, int maxElements, int startPosition, int endPosition)
        {
            var(entities, totalCount) = await _repository.GetAll(filter, maxElements, startPosition, endPosition);

            return _actionService.PartialResponse(entities.ToList(), startPosition, endPosition, totalCount);
        }

        public async Task<IBaseApiResponse> GetById(Guid id)
        {
            var user = await _repository.GetByFilter(new GetSingleOrganizationById(id));

            if (user == null)
            {
                return _actionService.NotFoundResponse(Errors.CannotFind(Entities.User(), "ID", $"{id}"));
            }

            return _actionService.OkResponse(_mapper.Map<UserDto>(user));
        }

        public async Task<IBaseApiResponse> IsOrganizationExists(string inn)
        {
            var user = await _repository.GetByFilter(new GetSingleOrganizationByInn(inn));

            return user != null ? _actionService.OkResponse(true) : _actionService.OkResponse(false);
        }

        public async Task<IBaseApiResponse> Update(OrganizationDto organization)
        {
            var model = _mapper.Map<Organization>(organization);

            model = await _repository.Update(model);

            if (model == null)
            {
                var errors = new List<string>
                {
                    Errors.CannotUpdate(Entities.Organization())
                };

                return _actionService.BadRequestResponse(errors);
            }
            else
            {
                return _actionService.OkResponse(_mapper.Map<OrganizationDto>(organization));
            }
        }
    }
}
