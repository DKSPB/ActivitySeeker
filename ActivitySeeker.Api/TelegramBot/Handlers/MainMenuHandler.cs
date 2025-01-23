using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using ActivitySeeker.Infrastructure.Models.Settings;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.MainMenu)]
public class MainMenuHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly ActivityPublisher _activityPublisher;
    private readonly ISettingsService _settingsService;
    private readonly Settings _settings;
    public MainMenuHandler (
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher, 
        ISettingsService settingsService, 
        IOptions<Settings> settingsOptions,
        IWebHostEnvironment webHostEnvironment) :
        base (userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _settings = settingsOptions.Value;
        _settingsService = settingsService;
        _activityPublisher = activityPublisher;
    }
    protected override async Task ActionsAsync(UserUpdate userUpdate)
    {
        ResponseMessageText = CurrentUser.State.ToString();
        ResponseImage = await GetImage();
        Keyboard = Keyboards.GetMainMenuKeyboard();
    }

    private async Task<byte[]> GetImage()
    {
        var fileName = _settings.TelegramBotSettings.MainMenuImageName;
        var filePath = _settingsService.CombinePathToFile(_webRootPath, fileName);

        return await _settingsService.GetImage(filePath);
    }
}