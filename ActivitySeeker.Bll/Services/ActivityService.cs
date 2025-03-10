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
                .Include(x => x.ActivityType).ToList();

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
            var result = _context.Activities.FromSqlInterpolated($"select * from activity_seeker.get_activities({requestParams.IsOnline}, {requestParams.ActivityTypeId}, {requestParams.SearchFrom}, {requestParams.SearchTo}, {requestParams.IsPublished}, {requestParams.CityId})");

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
            activityEntity.IsPublished = false;
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
                activityEntity.IsPublished = activity.OfferState;
                activityEntity.TgMessageId = activity.TgMessageId;
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<byte[]?> GetImage(Guid activityId)
        {
            return (await _context.Activities.FindAsync(activityId))?.Image;
        }

        /// <inheritdoc />
        public async Task PublishActivity(ActivityDto activity, int tgMessageId)
        {
            activity.OfferState = true;
            activity.TgMessageId = tgMessageId;
            await UpdateActivity(activity);
        }

        /// <inheritdoc />
        public async Task WithdrawFromPublication(ActivityDto activity)
        {
            activity.OfferState = false;
            activity.TgMessageId = null;
            await UpdateActivity(activity);
        }

        public async Task RemoveOldActivities()
        {
            var request = new ActivityRequest();
            var oldActivities = GetActivities(request)!
                .Where(x => DateTime.Compare(x.StartDate, DateTime.Now) <= 0);
            
            _context.RemoveRange(oldActivities);

            await _context.SaveChangesAsync();
        }
    }
}
