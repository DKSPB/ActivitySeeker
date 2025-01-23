using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using ActivitySeeker.Infrastructure.Models.Settings;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler : AbstractHandler
{
    private readonly Settings _settings;
    private readonly ISettingsService _settingsService;
    private readonly string _webRootPath;

    private const string MessageText = $"Перед началом использования бота выберите Ваш Город." +
                                   $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название, как текст сообщения" +
                                   $"\nВы всегда сможете изменить Ваш город в разделе:" +
                                   $"\nМеню > Выбрать город";
    
    private readonly ICityService _cityService;

    public StartHandler (
        ICityService cityService, 
        IUserService userService, 
        IActivityService activityService, 
        ISettingsService settingsService, 
        IOptions<Settings> optionsSettings,
        ActivityPublisher activityPublisher, 
        IWebHostEnvironment webHostEnvironment )
        : base (userService, activityService, activityPublisher)
    {
        _cityService = cityService;
        _settingsService = settingsService;
        _settings = optionsSettings.Value;
        _webRootPath = webHostEnvironment.WebRootPath;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.CityId is not null)
        {
            ResponseMessageText = CurrentUser.State.ToString();
            ResponseImage = await GetImage();
            Keyboard = Keyboards.GetMainMenuKeyboard();
            
            CurrentUser.State.StateNumber = StatesEnum.MainMenu;
        }
        else
        {
            var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;
            var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

            ResponseMessageText = MessageText;
            Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

            CurrentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
        }
    }

    private async Task<byte[]> GetImage()
    {
        var fileName = _settings.TelegramBotSettings.MainMenuImageName;
        var filePath = _settingsService.CombinePathToFile(_webRootPath, fileName);

        return await _settingsService.GetImage(filePath);
    }
}