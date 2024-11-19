using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ActivitySeeker.Bll.Services
{
    public class ActivityService: IActivityService
    {
        private readonly ActivitySeekerContext _context;
        public ActivityService(ActivitySeekerContext context)
        {
            _context = context;
        }
        
        /// <inheritdoc />
        public LinkedList<ActivityTelegramDto> GetActivitiesLinkedList(UserDto currentUser)
        {
            var currentUserState = currentUser.State;
            var activities = new LinkedList<ActivityTelegramDto>();
            
            var activityRequest = new ActivityRequest
            {
                IsOnline = currentUserState.ActivityFormat,
                ActivityTypeId = currentUserState.ActivityType.Id,
                SearchFrom = currentUserState.SearchFrom,
                SearchTo = currentUserState.SearchTo,
                IsPublished = true,
            };

            if (activityRequest.IsOnline.HasValue && !activityRequest.IsOnline.Value)
            {
                activityRequest.CityId = currentUser.CityId;
            }
            else
            {
                activityRequest.CityId = null;
            }

            var result = GetActivities(activityRequest)?
                .OrderBy(x => x.StartDate)
                .Include(x => x.ActivityType);

            if (result == null || !result.Any())
            {
                return activities;
            }
            
            foreach (var activity in result)
            {
                activities.AddLast(new ActivityTelegramDto(activity));
            }

            return activities;
        }

        /// <inheritdoc />
        public IQueryable<Activity>? GetActivities(ActivityRequest requestParams)
        {
            var result = _context.Activities
                .Where(x => x.ActivityTypeId == requestParams.ActivityTypeId || requestParams.ActivityTypeId == null)
                .Where(x => !requestParams.SearchFrom.HasValue ||
                            x.StartDate.CompareTo(requestParams.SearchFrom.Value.Date) >= 0)
                .Where(x => !requestParams.SearchTo.HasValue ||
                            x.StartDate.CompareTo(requestParams.SearchTo.Value.AddDays(1).Date) < 0)
                .Where(x => !requestParams.IsPublished.HasValue || x.IsPublished == requestParams.IsPublished)
                .Where(x => !requestParams.IsOnline.HasValue || x.IsOnline == requestParams.IsOnline)
                .Where(x => !requestParams.CityId.HasValue || x.CityId == requestParams.CityId);

            return result;
        }

        /// <inheritdoc />
        public async Task<ActivityDto> GetActivityAsync(Guid activityId)
        {
            var activityEntity = await _context.Activities
                .Include(x => x.ActivityType)
                .FirstOrDefaultAsync(x => x.Id.Equals(activityId));

            return activityEntity is null
                ? throw new NullReferenceException($"Активность с идентификатором {activityId} не найдена")
                : new ActivityDto(activityEntity);
        }
        
        /// <inheritdoc />
        public async Task<List<ActivityDto>> GetActivitiesByType(Guid activityTypeId)
        {
            return await _context.Activities.Where(x => x.ActivityTypeId.Equals(activityTypeId))
                .Select(x => new ActivityDto(x)).ToListAsync();
        }

        /// <inheritdoc />
        public async Task CreateActivity(ActivityDto activity)
        {
            var activityEntity = activity.ToActivity();
            
            await _context.Activities.AddAsync(activityEntity);
            await _context.SaveChangesAsync();
        }
        
        /// <inheritdoc />

        public async Task DeleteActivity(List<Guid> activityIds)
        {
            var activitiesForRemove =
                await _context.Activities.Where(x => activityIds.Contains(x.Id)).ToListAsync();

            _context.Activities.RemoveRange(activitiesForRemove);

            await _context.SaveChangesAsync();
        }
        
        /// <inheritdoc />
        public async Task UpdateActivity(ActivityDto activity)
        {
            var activityEntity = _context.Activities.FirstOrDefault(x => x.Id.Equals(activity.Id));

            if (activityEntity is not null)
            {
                activityEntity.LinkOrDescription = activity.LinkOrDescription;
                activityEntity.StartDate = activity.StartDate;
                activityEntity.ActivityTypeId = activity.ActivityTypeId;
                activityEntity.IsOnline = activity.IsOnline;
                activityEntity.CityId = activity.CityId;
                activityEntity.Image = activity.Image;
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<byte[]?> GetImage(Guid activityId)
        {
            return (await _context.Activities.FindAsync(activityId))?.Image;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ActivityDto>?> PublishActivities(List<Guid> activityIds)
        {
            var activityEntities = await _context.Activities
                .Include(x => x.ActivityType)
                .Where(x => activityIds.Contains(x.Id)).ToListAsync();

            activityEntities.ForEach(x => x.IsPublished = true);
            _context.Activities.UpdateRange(activityEntities);
            await _context.SaveChangesAsync();

            return activityEntities?.Select(x => new ActivityDto(x)).ToList();
        }
    }
}
