using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler : AbstractHandler
{
    private readonly BotConfiguration _botConfig;
    private readonly string _webRootPath;

    /*private const string MessageText = $"Перед началом использования бота выберите Ваш Город." +
                                   $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название, как текст сообщения" +
                                   $"\nВы всегда сможете изменить Ваш город в разделе:" +
                                   $"\nМеню > Выбрать город";*/
    
    private readonly ICityService _cityService;

    public StartHandler (
        ICityService cityService, 
        IUserService userService, 
        IActivityService activityService,
        IOptions<BotConfiguration> botConfigOptions,
        ActivityPublisher activityPublisher, 
        IWebHostEnvironment webHostEnvironment )
        : base (userService, activityService, activityPublisher)
    {
        _cityService = cityService;
        _botConfig = botConfigOptions.Value;
        _webRootPath = webHostEnvironment.WebRootPath;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.CityId is not null)
        {
            //var nextState = StatesEnum.MainMenu;
            CurrentUser.State.StateNumber = StatesEnum.MainMenu;//nextState;

            var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
            Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
            //Response.Text = CurrentUser.State.ToString();
            //Response.Keyboard = Keyboards.GetMainMenuKeyboard();
            //Response.Image = await GetImage(nextState.ToString());
        }
        else
        {
            var nextState = StatesEnum.SaveDefaultSettings;
            CurrentUser.State.StateNumber = nextState;
            
            var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;
            var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

            var saveDefaultSettingsState = new SaveDefaultSetting(_botConfig.RootImageFolder, _webRootPath);
            Response = await saveDefaultSettingsState.GetResponseMessage(mskId, spbId, false);

            /*Response.Text = MessageText;
            Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);
            Response.Image = await GetImage(nextState.ToString());*/
        }
    }

    /*private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }*/
}