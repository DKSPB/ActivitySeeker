using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SelectActivityFormat)]
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
            Response.Text = "Выбери формат проведения активности:";
            Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(true);
            Response.Image = await GetImage(StatesEnum.SelectActivityFormat.ToString());

            CurrentUser.State.StateNumber = StatesEnum.SaveActivityFormat;
        }

        private async Task<byte[]?> GetImage(string fileName)
        {
            var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

            return await FileProvider.GetImage(filePath);
        }
    }
}
