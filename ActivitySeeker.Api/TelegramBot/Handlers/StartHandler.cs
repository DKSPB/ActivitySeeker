using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.Start*/"/start")]
public class StartHandler : AbstractHandler
{
    private readonly BotConfiguration _botConfig;
    private readonly string _webRootPath;
    
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
            CurrentUser.State.StateNumber = StatesEnum.MainMenu;

            var mainMenuState = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
            Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
        }
        else
        {
            var nextState = StatesEnum.SaveDefaultSettings;
            CurrentUser.State.StateNumber = nextState;
            
            var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;
            var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

            var saveDefaultSettingsState = new SaveDefaultSetting(_botConfig.RootImageFolder, _webRootPath);
            Response = await saveDefaultSettingsState.GetResponseMessage(mskId, spbId, false);
        }
    }
}