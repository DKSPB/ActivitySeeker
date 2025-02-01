using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveDefaultSettings)]
public class SaveDefaultSettingsHandler : AbstractHandler
{
    private readonly ICityService _cityService;

    public SaveDefaultSettingsHandler(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher publisher)
        :base(userService, activityService, publisher)
    {
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (userData.CallbackQueryId is null)
        {
            var cityName = userData.Data;

            var cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            if (!cities.Any())
            {
                var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

                var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

                Response.Text = $"Поиск не дал результатов." +
                                      $"\nУточните название и попробуйте ещё раз";
                
                Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

            }
            else
            {
                Response.Text = $"Найденные города:";
                Response.Keyboard = Keyboards.GetCityKeyboard(cities.ToList());
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

            Response.Text = CurrentUser.State.ToString();
            Response.Keyboard = Keyboards.GetMainMenuKeyboard();
        }
    }
}