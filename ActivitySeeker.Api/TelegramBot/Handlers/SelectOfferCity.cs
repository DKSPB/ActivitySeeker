using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SelectOfferCity)]
public class SelectOfferCity : AbstractHandler
{
    private readonly ICityService _cityService;
    private int _mskId = -1;
    private int _spbId = -1;
    private List<City> _cities = new ();

    public SelectOfferCity(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    {
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (userData.CallbackQueryId is null)
        {
            var cityName = userData.Data;

            _cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            if (!_cities.Any())
            {
                Response.Text = $"Поиск не дал результата." +
                                      $"\nУточните название и попробуйте ещё раз.";
                
                Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(_mskId, _spbId, false);

                _mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;
                _spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;
            }
            else
            {
                Response.Text = $"Найденные города:";
                Response.Keyboard = Keyboards.GetCityKeyboard(_cities);
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

            if(cityId == -1)
            {
                CurrentUser.Offer.CityId = CurrentUser.CityId;
            }
            else if (cityId != -1)
            {
                CurrentUser.Offer.CityId = cityId;
            }

            Response.Text = "Заполни описание активности:";
            Response.Keyboard = Keyboards.GetEmptyKeyboard();
            
            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        }
    }
}