using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using ApplicationServices.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationServices.Implementation;

public class ActivityTypeService : IActivityTypeService
{
    private readonly IDbContext _context;
    
    public ActivityTypeService(IDbContext context)
    {
        _context = context;
    }

    public IQueryable<ActivityType> GetAll()
    {
        return _context.ActivityTypes.AsNoTracking()
            .Include(x => x.Parent)
            .Include(z => z.Children);
    }
}