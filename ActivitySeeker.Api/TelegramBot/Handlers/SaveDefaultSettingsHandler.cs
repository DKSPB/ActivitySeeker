using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveDefaultSettings)]
public class SaveDefaultSettingsHandler : AbstractHandler
{
    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _publisher;

    private InlineKeyboardMarkup _keyboard = Keyboards.GetEmptyKeyboard();
    
    public SaveDefaultSettingsHandler(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher publisher)
        :base(userService, activityService, publisher)
    {
        _cityService = cityService;
        _userService = userService;
        _publisher = publisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (userData.CallbackQueryId is null)
        {
            var cityName = userData.Data;

            var cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            string msgText;

            if (!cities.Any())
            {
                msgText = $"Поиск не дал результатов." +
                          $"\nУточните название и попробуйте ещё раз                                                                                                             ";

                var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

                var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

                ResponseMessageText = msgText;
                _keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

            }
            else
            {
                ResponseMessageText = $"Найденные города:";

                _keyboard = Keyboards.GetCityKeyboard(cities.ToList());
            }
        }
        else
        {
            var cityIdString = userData.Data;

            var parseResult = int.TryParse(cityIdString, out var cityId);

            if (!parseResult)
            {
                throw new ArgumentNullException($"Не удалось распознать идентификатор города: {cityIdString}");
            }

            var city = await _cityService.GetById(cityId);

            if (city is null)
            {
                throw new NullReferenceException("По заданному идентификатору городов не обнаружено");
            }

            CurrentUser.CityId = cityId;
            CurrentUser.State.StateNumber = StatesEnum.MainMenu;

            ResponseMessageText = CurrentUser.State.ToString();
            _keyboard = Keyboards.GetMainMenuKeyboard();
        }
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return _keyboard;
    }
}