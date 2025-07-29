using MediatR;
using AutoMapper;
using UseCases.Common;
using DataAccess.Interfaces;
using UseCases.Activity.Models;
using Microsoft.EntityFrameworkCore;


namespace UseCases.Activity.Queries.GetAll;

internal class GetActivitiesHandler : IRequestHandler<GetActivitiesQuery, PagedResult<ActivityDto>>
{
    private readonly IMapper _mapper;
    private readonly IDbContext _context;

    public GetActivitiesHandler(IMapper mapper, IDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<PagedResult<ActivityDto>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
    {
        var entities = _context.Activities;
            
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