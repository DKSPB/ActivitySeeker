using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler : AbstractHandler
{
    private readonly BotConfiguration _botConfig;
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
        IOptions<BotConfiguration> botConfigOptions,
        ActivityPublisher activityPublisher, 
        IWebHostEnvironment webHostEnvironment )
        : base (userService, activityService, activityPublisher)
    {
        _cityService = cityService;
        _settingsService = settingsService;
        _botConfig = botConfigOptions.Value;
        _webRootPath = webHostEnvironment.WebRootPath;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.CityId is not null)
        {
            ResponseMessageText = CurrentUser.State.ToString();
            
            Keyboard = Keyboards.GetMainMenuKeyboard();

            var nextState = StatesEnum.MainMenu;
            ResponseImage = await GetImage(nextState);
            CurrentUser.State.StateNumber = nextState;
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

    private async Task<byte[]> GetImage(StatesEnum state)
    {
        var fileName = state;
        var filePath = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName.ToString());

        return await _settingsService.GetImage(filePath);
    }
}