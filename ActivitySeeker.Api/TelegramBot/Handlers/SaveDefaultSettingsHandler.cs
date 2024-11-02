using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveDefaultSettings)]
public class SaveDefaultSettingsHandler: IHandler
{
    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _publisher;
    
    public SaveDefaultSettingsHandler(ICityService cityService, IUserService userService, ActivityPublisher publisher)
    {
        _cityService = cityService;
        _userService = userService;
        _publisher = publisher;
    }
    public async Task HandleAsync(UserDto currentUser, UserUpdate userData)
    {
        Message message;

        if (userData.CallbackQueryId is null)
        {
            var cityName = userData.Data;

            var cities = (await _cityService.GetCitiesByName(cityName)).ToList();

            string msgText;

            if (!cities.Any())
            {
                msgText = $"Поиск не дал результатов." +
                          $"\nУточните название и попробуйте ещё раз";

                var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

                var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

                await _publisher.EditMessageAsync(
                    userData.ChatId,
                    currentUser.State.MessageId,
                    Keyboards.GetEmptyKeyboard());

                message = await _publisher.SendMessageAsync(
                    userData.ChatId,
                    msgText,
                    null,
                    Keyboards.GetDefaultSettingsKeyboard(mskId, spbId));
            }
            else
            {
                await _publisher.EditMessageAsync(
                    userData.ChatId,
                    currentUser.State.MessageId,
                    Keyboards.GetEmptyKeyboard());

                msgText = $"Найденные города:";

                message = await _publisher.SendMessageAsync(
                    userData.ChatId,
                    msgText,
                    null,
                    Keyboards.GetCityKeyboard(cities.ToList())
                );
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
            
            currentUser.CityId = cityId;
            currentUser.State.StateNumber = StatesEnum.MainMenu;
            
            await _publisher.EditMessageAsync(
                userData.ChatId,
                currentUser.State.MessageId, 
                Keyboards.GetEmptyKeyboard());

            message = await _publisher.SendMessageAsync(
                userData.ChatId,
                currentUser.State.ToString(),
                null,
                Keyboards.GetMainMenuKeyboard());
        }

        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}