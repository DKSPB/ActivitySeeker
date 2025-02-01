using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.ListOfChildrenActivities)]
    public class ListOfChildrenActivitiesHandler : ListOfActivitiesHandler
    {
        public ListOfChildrenActivitiesHandler(
            IUserService userService,
            IActivityService activityService,
            IActivityTypeService activityTypeService,
            ActivityPublisher activityPublisher,
            IWebHostEnvironment webHostEnvironment,
            IOptions<BotConfiguration> botConfigOptions)
            : base( userService,
                  activityService,
                  activityTypeService,
                  activityPublisher,
                  webHostEnvironment,
                  botConfigOptions)
        {}
    }
}
