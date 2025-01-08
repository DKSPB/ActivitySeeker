using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: AbstractHandler
{
    private const string MessageText = $"Перед началом использования бота выберите Ваш Город." +
                                   $"\nЕсли Ваш город не Москва или Санкт-Петербург, введите название, как текст сообщения" +
                                   $"\nВы всегда сможете изменить Ваш город в разделе:" +
                                   $"\nМеню > Выбрать город";

    private InlineKeyboardMarkup _keyboard = Keyboards.GetMainMenuKeyboard();

    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public StartHandler(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    {
        _userService = userService;
        _cityService = cityService;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.CityId is not null)
        {
            ResponseMessageText = CurrentUser.State.ToString();

            CurrentUser.State.StateNumber = StatesEnum.MainMenu;
        }
        else
        {
            var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

            var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

            ResponseMessageText = MessageText;

            _keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

            CurrentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
        }
    }
    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return _keyboard;
    }
}