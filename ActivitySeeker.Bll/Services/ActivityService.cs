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

        public LinkedList<ActivityDto> GetActivities(ActivityRequest requestParams)
        {
            var activities = new LinkedList<ActivityDto>();

            foreach (var activity in _context.Activities.ToList())
            {
                activities.AddLast(new ActivityDto(activity));
            }

            return activities;
        }
    }
}
