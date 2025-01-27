using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.MainMenu)]
public class MainMenuHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly ISettingsService _settingsService;
    public MainMenuHandler (
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher, 
        ISettingsService settingsService, 
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) :
        base (userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _settingsService = settingsService;
        _botConfig = botConfigOptions.Value;
    }
    protected override async Task ActionsAsync(UserUpdate userUpdate)
    {
        var nextState = StatesEnum.MainMenu;
        CurrentUser.State.StateNumber = nextState;

        Response.Text = CurrentUser.State.ToString();
        Response.Image = await GetImage(nextState);
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();
    }

    private async Task<byte[]?> GetImage(StatesEnum state)
    {
        var fileName = state.ToString();
        var filePath = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await _settingsService.GetImage(filePath);
    }
}