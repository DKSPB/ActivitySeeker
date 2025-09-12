using UseCases.Interfaces.Repos;

namespace UseCases.ActivityType.Queries.GetAll
{
    using AutoMapper;
    using MediatR;
    using Models;
    using Common;
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
            var entities = _context.ActivityTypes;
            
            var total = await entities.CountAsync(cancellationToken);

            var items = await entities
                .Skip(Math.Max(0, (request.Offset - 1) * request.Limit))
                .Take(request.Limit)
                .ToListAsync(cancellationToken);
        
            return new PagedResult<ActivityTypeDto>
            {
                Items = _mapper.Map<List<ActivityTypeDto>>(items),
                Total = total
            };
        }
    }
}
