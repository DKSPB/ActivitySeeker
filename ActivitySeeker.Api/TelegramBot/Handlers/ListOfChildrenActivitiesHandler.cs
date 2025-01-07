using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.ListOfChildrenActivities)]
    public class ListOfChildrenActivitiesHandler : ListOfActivitiesHandler
    {
        public ListOfChildrenActivitiesHandler(IUserService userService, IActivityService activityService, IActivityTypeService activityTypeService, ActivityPublisher activityPublisher)
            : base(userService, activityService, activityTypeService, activityPublisher)
        {
        }
    }
}
