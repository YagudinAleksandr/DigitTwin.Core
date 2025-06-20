using AutoMapper;
using DigitTwin.Core.ActionService;
using DigitTwin.Lib.Abstractions;
using DigitTwin.Lib.Contracts;
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

        public Task<IBaseApiResponse> Create(OrganizationCreateRequestDto organization)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> GetAllByFilter(Filter filter, int maxElements, int startPosition, int endPosition)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> IsOrganizationExists(string inn)
        {
            throw new NotImplementedException();
        }

        public Task<IBaseApiResponse> Update(OrganizationDto organization)
        {
            throw new NotImplementedException();
        }
    }
}
