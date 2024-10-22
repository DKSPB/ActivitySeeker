using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityTypeService
{
    Task<List<ActivityTypeDto>> GetTypes();

    Task<ActivityTypeDto> GetById(Guid id);

    Task Create(ActivityTypeDto activityType);

    Task Update(ActivityTypeDto activityType);

    Task Delete(List<Guid> activityTypeIds);
}