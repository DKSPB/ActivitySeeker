using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services;

public class ActivityTypeService: IActivityTypeService
{
    private readonly ActivitySeekerContext _context;
    
    public ActivityTypeService(ActivitySeekerContext context)
    {
        _context = context;
    }
    
    public async Task<List<ActivityTypeDto>> GetTypes()
    {
        return await _context.ActivityTypes.Select(x => new ActivityTypeDto(x)).ToListAsync();
    }

    public async Task<ActivityTypeDto> GetById(Guid id)
    {
        var activityTypeEntity = await _context.ActivityTypes.FirstOrDefaultAsync(x => x.Id == id);

        if (activityTypeEntity is null)
        {
            throw new NullReferenceException($"Тип активности с идентификатором {id} не найден");
        }

        return new ActivityTypeDto(activityTypeEntity);
    }

    public async Task Create(ActivityTypeDto activityType)
    {

            await _context.ActivityTypes.AddAsync(activityType.ToActivityType());
            await _context.SaveChangesAsync();

    }

    public async Task Update(ActivityTypeDto activityType)
    {

            var activityTypeEntity = _context.ActivityTypes.FirstOrDefault(x => x.Id == activityType.Id);

            if (activityTypeEntity is null)
            {
                throw new NullReferenceException($"Тип активности с идентификатором {activityType.Id} не найден");
            }

            activityTypeEntity.TypeName = activityType.TypeName;
            _context.ActivityTypes.Update(activityTypeEntity);
            await _context.SaveChangesAsync();

    }
    
    public async Task Delete(List<ActivityTypeDto> activityTypes)
    {

            foreach (var type in activityTypes)
            {
                var typeEntity = await _context.ActivityTypes.FirstOrDefaultAsync(x => x.Id == type.Id);

                if (typeEntity is not null)
                {
                    _context.ActivityTypes.Remove(typeEntity);
                }
            }

            await _context.SaveChangesAsync();

    }
}