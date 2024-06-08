using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityService
{
    LinkedList<ActivityDto> GetActivities(ActivityRequest requestParams);

    List<ActivityType> GetActivityTypes();

    ActivityType FindActivityType(Guid id);
}