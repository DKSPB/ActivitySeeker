using MediatR;
using AutoMapper;
using UseCases.Common;
using DataAccess.Interfaces;
using UseCases.Activity.Models;
using Microsoft.EntityFrameworkCore;
using UseCases.Interfaces.Common;


namespace UseCases.Activity.Queries.GetAll;

internal class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, PagedResult<ActivityDto>>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _context;
    private readonly IDateTimeConverter _timeConverter;

    public GetActivitiesHandler(IMapper mapper, IDbContext context, IDateTimeConverter timeConverter)
    {
        _mapper = mapper;
        _context = context;
        _timeConverter = timeConverter;
    }

    public async Task<PagedResult<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var searchFrom = _timeConverter.ToUtc(request.SearchFrom, request.Timestamp);
        var searchBy = _timeConverter.ToUtc(request.SearchTo, request.Timestamp);
        
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
            .ToListAsync(cancellationToken);
        
        return new PagedResult<ActivityDto>
        {
            Items = _mapper.Map<List<ActivityDto>>(items),
            Total = total
        };
    }
}