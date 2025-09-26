namespace UseCases.ActivityType.Queries.GetAll
{
    using AutoMapper;
    using MediatR;
    using Models;
    using Common;
    using Interfaces.Repos;
    using Domain.Entities;
    public class GetActivityTypesHandler : IRequestHandler<GetActivityTypesQuery, PagedResult<ActivityTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IActivityTypeRepository _repository;
        public GetActivityTypesHandler(IMapper mapper, IActivityTypeRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<PagedResult<ActivityTypeDto>> Handle(GetActivityTypesQuery request, CancellationToken cancellationToken)
        {
            var spec = new PagingSpecification<ActivityType>(request.Limit, request.Offset);
            
            var entities = await _repository.GetAllAsync(spec, cancellationToken);
            
            return new PagedResult<ActivityTypeDto>
            {
                Items = _mapper.Map<List<ActivityTypeDto>>(entities.Items),
                Total = entities.Total
            };
        }
    }
}
