namespace UseCases.Activity.Queries.GetAll
{
    using Common;
    using Models;
    using MediatR;
    using AutoMapper;
    using Domain.Entities;
    using Interfaces.Repos;
    using UseCases.Interfaces.Common;
    internal class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, PagedResult<ActivityDto>>
    {
        private readonly IMapper _mapper;
        private readonly IActivityRepository _repository;
        private readonly IDateTimeConverter _timeConverter;

        public GetActivitiesHandler(IMapper mapper, IActivityRepository repository, IDateTimeConverter timeConverter)
        {
            _mapper = mapper;
            _repository = repository;
            _timeConverter = timeConverter;
        }

        public async Task<PagedResult<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            var searchFrom = _timeConverter.ToUtc(request.SearchFrom, request.Timestamp);
            var searchBy = _timeConverter.ToUtc(request.SearchTo, request.Timestamp);

            var spec = new PagingSpecification<Activity>(request.Limit, request.Offset);
            var result = await _repository.GetAllAsync(spec, cancellationToken);
            /*_repository.
        
                var entities = _context.Activities
                    .Include(x => x.ActivityType)
                    .Where(x =>
                        ((x.UserId == request.UserId) || request.UserId == null) &&
                        ((x.ActivityTypeId == request.ActivityTypeId) || request.ActivityTypeId == null) &&
                        ((x.CityId == request.CityId) || request.CityId == null) &&
                        ((x.IsOnline == request.IsOnline) || request.IsOnline == null) &&
                        ((x.IsPublished == request.IsPublished) || request.IsPublished == null) &&
                        (searchBy == null || x.StartDate <= searchBy) &&
                        (searchFrom == null || x.EndDate >= searchFrom)
                    );

            var total = await entities.CountAsync(cancellationToken);

            var items = await entities
                .OrderBy(x => x.StartDate)
                .Skip(Math.Max(0, (request.Offset - 1) * request.Limit))
                .Take(request.Limit)
                .ToListAsync(cancellationToken);*/
        
            return new PagedResult<ActivityDto>
            {
                Items = _mapper.Map<List<ActivityDto>>(result.Items),
                Total = result.Total
            };
        }
    }
}