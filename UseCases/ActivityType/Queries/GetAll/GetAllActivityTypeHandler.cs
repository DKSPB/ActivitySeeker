using ActivitySeeker.Bll.Models;
using Infrastructure.Interfaces.Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.ActivityType.Queries.GetAll;

public class GetAllActivityTypeHandler: IRequestHandler<GetAllActivityTypeQuery, List<ActivityTypeDto>>
{
    private readonly IDbContext _context;

    public GetAllActivityTypeHandler(IDbContext context)
    {
        _context = context;
    }
    
    public Task<List<ActivityTypeDto>> Handle(GetAllActivityTypeQuery request, CancellationToken cancellationToken)
    {
        return _context.ActivityTypes
            .Include(x => x.Parent)
            .Include(z => z.Children);
    }
}