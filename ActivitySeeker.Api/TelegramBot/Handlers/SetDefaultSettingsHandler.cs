using System.Text;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SetDefaultSettings)]
public class SetDefaultSettingsHandler: AbstractHandler
{
    private readonly ICityService _cityService;

    public SetDefaultSettingsHandler(ICityService cityService, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    {
        _cityService = cityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

        var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

        var currentCity = "не задан";

        if (CurrentUser.CityId.HasValue)
        {
            var userCity = await _cityService.GetById(CurrentUser.CityId.Value);

            if (userCity is not null)
            {
                currentCity = userCity.Name;
            }
        }

        Response.Text = CreateResponseMessage(currentCity);
        Response.Keyboard = Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false);

        CurrentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
    }

    private string CreateResponseMessage(string currentCity)
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine("Задайте Ваш Город.");
        stringBuilder.AppendLine("Если Ваш город не Москва и Санкт-Петербург, введите его название как текст сообщения.");
        stringBuilder.AppendLine($"Текущий город: {currentCity}.");
        return stringBuilder.ToString();
    }
}