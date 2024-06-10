using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Services
{
    public class ActivityService: IActivityService
    {
        private readonly ActivitySeekerContext _context;
        public ActivityService(ActivitySeekerContext context)
        {
            _context = context;
        }

        public List<ActivityType> GetActivityTypes()
        {
            return _context.ActivityTypes.ToList();
        }

        public ActivityType FindActivityType(Guid id) 
        {
            var activityType = _context.ActivityTypes.Find(id);
            return activityType ?? throw new NullReferenceException($"activity type with {id} is null");
        }

        public LinkedList<ActivityDto> GetActivities(ActivityRequest requestParams)
        {
            var activities = new LinkedList<ActivityDto>();

            var result = _context.Activities
                .Where(x => x.ActivityTypeId == requestParams.ActivityTypeId || requestParams.ActivityTypeId == null)
                .Where(x => x.StartDate.CompareTo(requestParams.SearchFrom) >= 0 && x.StartDate.CompareTo(requestParams.SearchTo) <= 0)
                .ToList();

            foreach (var activity in result)
            {
                activities.AddLast(new ActivityDto(activity));
            }

            return activities;
        }
    }
}
