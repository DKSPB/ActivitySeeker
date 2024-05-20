using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityService
{
    LinkedList<ActivityDto> GetActivities(ActivityRequest requestParams);
}