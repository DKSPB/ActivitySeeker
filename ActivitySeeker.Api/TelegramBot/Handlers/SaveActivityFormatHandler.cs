using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

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
            if (userData.Data.Equals("online"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = true;
                CurrentUser.State.StateNumber = nextState;

                Response.Text = CurrentUser.State.ToString();
                Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                Response.Image = await GetImage(nextState.ToString());
            }
            else if (userData.Data.Equals("offline"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = false;
                CurrentUser.State.StateNumber = nextState;

                Response.Text = CurrentUser.State.ToString();
                Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                Response.Image = await GetImage(nextState.ToString());
            }

            else if (userData.Data.Equals("any"))
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.ActivityFormat = null;
                CurrentUser.State.StateNumber = nextState;

                Response.Text = CurrentUser.State.ToString();
                Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                Response.Image = await GetImage(nextState.ToString());
            }
            else
            {
                var nextState = StatesEnum.SelectActivityFormat;
                CurrentUser.State.StateNumber = nextState;

                Response.Text = "Выберите формат проведения активности:";
                Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(true);
                Response.Image = await GetImage(nextState.ToString());
            }
        }

        private async Task<byte[]?> GetImage(string fileName)
        {
            var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

            return await FileProvider.GetImage(filePath);
        }
    }
}
