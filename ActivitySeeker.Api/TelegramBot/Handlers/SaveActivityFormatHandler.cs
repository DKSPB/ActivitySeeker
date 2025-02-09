using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using Microsoft.Extensions.Options;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;


namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SaveActivityFormat)]
    public class SaveActivityFormatHandler : AbstractHandler
    {
        private readonly string _webRootPath;
        private readonly BotConfiguration _botConfig;

        public SaveActivityFormatHandler (
            IUserService userService,
            IActivityService activityService,
            ActivityPublisher activityPublisher,
            IWebHostEnvironment webHostEnvironment,
            IOptions<BotConfiguration> botConfigOptions)
            : base (userService, activityService, activityPublisher)
        {
            _webRootPath = webHostEnvironment.WebRootPath;
            _botConfig = botConfigOptions.Value;
        }

        protected override async Task ActionsAsync(UserUpdate userData)
        {
            var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
            var activityFormatChapterState = new ActivityFormatChapter(_botConfig.RootImageFolder, _webRootPath);

            if (userData.Data.Equals("online"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = true;
                CurrentUser.State.StateNumber = nextState;

                Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
            }
            else if (userData.Data.Equals("offline"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = false;
                CurrentUser.State.StateNumber = nextState;

                Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
            }

            else if (userData.Data.Equals("any"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = null;
                CurrentUser.State.StateNumber = nextState;

                Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
            }
            else
            {
                CurrentUser.State.StateNumber = StatesEnum.SelectActivityFormat;

                Response = await activityFormatChapterState.GetResponseMessage(true);
            }
        }
    }
}
