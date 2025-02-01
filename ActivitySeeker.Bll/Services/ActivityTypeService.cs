using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services;

public class ActivityTypeService: IActivityTypeService
{
    private readonly ActivitySeekerContext _context;
    
    public ActivityTypeService(ActivitySeekerContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task<List<ActivityTypeDto>> GetAll()
    {
        return await GetActivityTypes()
            .Select(x => new ActivityTypeDto(x))
            .ToListAsync();
    }

    private IQueryable<ActivityType> GetActivityTypes()
    {
        return _context.ActivityTypes
            .Include(x => x.Parent)
            .Include(z => z.Children);
    }

    /// <inheritdoc />
    public async Task<ActivityTypeDto> GetById(Guid id)
    {
        var activityTypeEntity = await GetActivityTypes().FirstOrDefaultAsync(x => x.Id == id);

        if (activityTypeEntity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {id} не найден");
        }

        return new ActivityTypeDto(activityTypeEntity);
    }

    /// <inheritdoc />
    public async Task Create(ActivityTypeDto activityType)
    {
        await _context.ActivityTypes.AddAsync(activityType.ToActivityType());
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Update(ActivityTypeDto activityType)
    {
        var activityTypeEntity = await GetActivityTypes().FirstOrDefaultAsync(x => x.Id == activityType.Id);

        if (activityTypeEntity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {activityType.Id} не найден");
        }

        activityTypeEntity.TypeName = activityType.TypeName;
        activityTypeEntity.ParentId = activityType.ParentId;

        _context.ActivityTypes.Update(activityTypeEntity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task Delete(List<Guid> activityTypeIds)
    {
        var activityEntities = _context.ActivityTypes.Where(x => activityTypeIds.Contains(x.Id));

        _context.ActivityTypes.RemoveRange(activityEntities);

        await _context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UploadImage(Guid activityTypeId, string path, Stream image)
    {
        var activityType = await _context.ActivityTypes.FirstAsync(x => x.Id == activityTypeId);

        activityType.ImagePath = path;

        await FileProvider.UploadImage(path, image);

        await _context.SaveChangesAsync();
    }
}