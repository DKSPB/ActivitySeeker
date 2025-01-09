using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: AbstractHandler
{
    private const string MessageText = $"Перед началом использования бота выберите Ваш Город." +
                                   $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название, как текст сообщения" +
                                   $"\nВы всегда сможете изменить Ваш город в разделе:" +
                                   $"\nМеню > Выбрать город";
    
    private readonly ICityService _cityService;

    public StartHandler(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    {
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.CityId is not null)
        {
            ResponseMessageText = CurrentUser.State.ToString();
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
}