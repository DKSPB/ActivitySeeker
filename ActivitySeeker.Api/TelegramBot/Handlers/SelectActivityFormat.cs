using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using Microsoft.Extensions.Options;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(/*StatesEnum.SelectActivityFormat*/"/select_activity_format")]
    public class SelectActivityFormat : AbstractHandler
    {
        private readonly string _webRootPath;
        private readonly BotConfiguration _botConfig;

        public SelectActivityFormat(
            IUserService userService,
            IActivityService activityService,
            ActivityPublisher activityPublisher,
            IWebHostEnvironment webHostEnvironment,
            IOptions<BotConfiguration> botConfigOptions) 
            : base ( userService,
                  activityService,
                  activityPublisher) 
        {
            _webRootPath = webHostEnvironment.WebRootPath;
            _botConfig = botConfigOptions.Value;
        }

        protected override async Task ActionsAsync(UserUpdate userData)
        {
            var activityFormatChapterState = new ActivityFormatChapter(_botConfig.RootImageFolder, _webRootPath);
            Response = await activityFormatChapterState.GetResponseMessage(true);

            CurrentUser.State.StateNumber = StatesEnum.SaveActivityFormat;
        }
    }
}
