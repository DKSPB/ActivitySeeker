using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SelectActivityFormat)]
    public class SelectActivityFormat : AbstractHandler
    {
        private readonly string _webRootPath;
        private readonly BotConfiguration _botConfig;
        private readonly ISettingsService _settingsService;

        public SelectActivityFormat(
            IUserService userService,
            IActivityService activityService,
            ActivityPublisher activityPublisher,
            ISettingsService settingsService,
            IWebHostEnvironment webHostEnvironment,
            IOptions<BotConfiguration> botConfigOptions) 
            : base ( userService,
                  activityService,
                  activityPublisher) 
        {
            _webRootPath = webHostEnvironment.WebRootPath;
            _settingsService = settingsService;
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
            var filePath = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

            return await _settingsService.GetImage(filePath);
        }
    }
}
