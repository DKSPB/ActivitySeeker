using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SelectOfferCity)]
public class SelectOfferCity : AbstractHandler
{
    private readonly ICityService _cityService;
    private int _mskId = -1;
    private int _spbId = -1;
    private bool _anyCity = false;
    private List<City> _cities = new List<City>();
    private string? _callbackQueryId;

    public SelectOfferCity(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    {
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        _callbackQueryId = userData.CallbackQueryId;
        if (_callbackQueryId is null)
        {
            var cityName = userData.Data;

            _cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            _anyCity = _cities.Any();

            if (!_anyCity)
            {
                ResponseMessageText = $"Поиск не дал результата." +
                                      $"\nУточните название и попробуйте ещё раз.";

                _mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

                _spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;
            }
            else
            {
                ResponseMessageText = $"Найденные города:";
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

            if (city is null && cityId != -1)
            {
                throw new NullReferenceException("По заданному идентификатору городов не обнаружено");
            }

            ResponseMessageText = "Заполни описание активности:";
            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        }
    }

    protected override IReplyMarkup GetKeyboard()
    {
        if (!string.IsNullOrEmpty(_callbackQueryId))
        {
            return Keyboards.GetEmptyKeyboard();
        }
        return !_anyCity ? 
            Keyboards.GetDefaultSettingsKeyboard(_mskId, _spbId, false) : 
            Keyboards.GetCityKeyboard(_cities);
    }
}