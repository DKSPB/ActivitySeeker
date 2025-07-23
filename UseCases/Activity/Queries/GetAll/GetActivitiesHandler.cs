using AutoMapper;
using DataAccess.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UseCases.Activity.Models;
using UseCases.Common;

namespace UseCases.Activity.Queries.GetAll;

public class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, PagedResult<ActivityDto>>
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GetActivitiesHandler(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedResult<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Activities.AsQueryable();
            
        var total = await entities.CountAsync(cancellationToken);
            
        var items = await entities.OrderBy(x => x.StartDate)
            .Skip((request.Offset - 1) * request.Limit)
            .Take(request.Limit)
            .ToListAsync(cancellationToken);
        
        return new PagedResult<ActivityDto>
        {
            Items = _mapper.Map<List<ActivityDto>>(items),
            Total = total
        };
    }
}