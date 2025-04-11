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

    public async Task Create(ActivityType activityType, CancellationToken cancellationToken = default)
    {
        await _context.ActivityTypes.AddAsync(activityType, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(ActivityType activityType, CancellationToken cancellationToken = default)
    {
        var entity = await _context.ActivityTypes
            .FirstOrDefaultAsync(x => x.Id == activityType.Id, cancellationToken);
        
        if (entity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {activityType.Id} не найден");
        }

        entity.TypeName = activityType.TypeName;
        entity.ParentId = activityType.ParentId;
        
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(List<Guid> activityTypeIds, CancellationToken cancellationToken = default)
    {
        var activityTypeEntities = _context.ActivityTypes.Where(x => activityTypeIds.Contains(x.Id));

        _context.ActivityTypes.RemoveRange(activityTypeEntities);

        await _context.SaveChangesAsync(cancellationToken);
    }
}