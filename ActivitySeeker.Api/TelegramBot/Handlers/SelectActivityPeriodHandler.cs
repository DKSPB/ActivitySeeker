using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityPeriodChapter)]
public class SelectActivityPeriodHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly ISettingsService _settingsService;

    public SelectActivityPeriodHandler(
        IUserService userService,
        IActivityService activityService,
        ActivityPublisher activityPublisher,
        ISettingsService settingsService,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) : 
        base ( userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _settingsService = settingsService;
        _botConfig = botConfigOptions.Value;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var nextState = StatesEnum.ActivityPeriodChapter;
        CurrentUser.State.StateNumber = nextState;

        Response.Text = "Выбери период проведения активности:";
        Response.Keyboard = Keyboards.GetPeriodActivityKeyboard();
        Response.Image = await GetImage(nextState.ToString());
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await _settingsService.GetImage(filePath);
    }
}