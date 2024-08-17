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
        public List<ActivityType> GetActivityTypes()
        {
            return _context.ActivityTypes.ToList();
        }

        /// <inheritdoc />
        public ActivityType GetActivityType(Guid activityId) 
        {
            var activityType = _context.ActivityTypes.FirstOrDefault(x => x.Id.Equals(activityId));
            return activityType ?? throw new NullReferenceException($"activity type with {activityId} is null");
        }

        /// <inheritdoc />
        public LinkedList<ActivityTelegramDto> GetActivitiesLinkedList(State currentUserState)
        {
            var activities = new LinkedList<ActivityTelegramDto>();
            
            var activityRequest = new ActivityRequest
            {
                ActivityTypeId = currentUserState.ActivityType.Id,
                SearchFrom = currentUserState.SearchFrom,
                SearchTo = currentUserState.SearchTo
            };

            var result = GetActivitiesByFilters(activityRequest);

            if (result is null)
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
        public List<ActivityDto>? GetActivities(ActivityRequest request)
        {
            var entityRequest = GetActivitiesByFilters(request);

            return entityRequest?.Select(x => new ActivityDto(x)).ToList();
        }

        /// <inheritdoc />
        public async Task<ActivityDto> GetActivity(Guid activityId)
        {
            var activityEntity = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id.Equals(activityId));

            var imageId = _context.Images
                .Where(x => x.ActivityId.Equals(activityId))
                .Select(x => new ImageDto{Id = x.Id})
                .ToList();

            if (activityEntity is null)
            {
                throw new NullReferenceException($"Активность с идентификатором {activityId} не найдена");
            }
            
            ActivityDto activityDto = new (activityEntity)
            {
                Images = imageId
            };

            return activityDto;
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
        public async Task DeleteActivity(List<ActivityDto> activitiesForRemove)
        {

                foreach (var activity in activitiesForRemove)
                {
                    var activityEntity =
                        _context.Activities.FirstOrDefault(x => x.ActivityTypeId.Equals(activity.ActivityTypeId));

                    if (activityEntity is not null)
                    {
                        _context.Activities.Remove(activityEntity);
                    }
                }

                await _context.SaveChangesAsync();

        }
        
        /// <inheritdoc />
        public async Task UpdateActivity(ActivityDto activity)
        {

            var activityEntity = _context.Activities.FirstOrDefault(x => x.Id.Equals(activity.Id));

            if (activityEntity is not null)
            {
                activityEntity.Description = activity.Description;
                activityEntity.StartDate = activity.StartDate;
                activityEntity.ActivityTypeId = activity.ActivityTypeId;
                activityEntity.Link = activity.Link;
                activityEntity.Images = activity.Images?.Select(x => x.ToEntity()).ToList();
            }

            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ImageDto>?> GetImages(Guid activityId)
        {
            var activity = await _context.Activities.FindAsync(activityId);
            var images = activity?.Images?.Select(x => new ImageDto(x));
            return images;
        }

        #region Private methods

        /// <summary>
        /// Получение списка активностей
        /// </summary>
        /// <param name="requestParams">Объект, содержащий запрос пользователя</param>
        /// <returns>Список активностей</returns>
        private IQueryable<Activity>? GetActivitiesByFilters(ActivityRequest requestParams)
        {
            var result = _context.Activities.Include(x => x.Images)
                .Where(x => x.ActivityTypeId == requestParams.ActivityTypeId || requestParams.ActivityTypeId == null)
                .Where(x => !requestParams.SearchFrom.HasValue || x.StartDate.CompareTo(requestParams.SearchFrom.Value.Date) >= 0)
                .Where(x => !requestParams.SearchTo.HasValue || x.StartDate.CompareTo(requestParams.SearchTo.Value.AddDays(1).Date) < 0);

            return result;
        }

        #endregion
    }
}
