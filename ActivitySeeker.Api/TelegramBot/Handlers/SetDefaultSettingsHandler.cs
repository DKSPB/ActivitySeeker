using System.Text;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SetDefaultSettings)]
public class SetDefaultSettingsHandler: IHandler
{
    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;

    public SetDefaultSettingsHandler(ICityService cityService, IUserService userService, ActivityPublisher activityPublisher)
    {
        _cityService = cityService;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, UserUpdate userData)
    {

        await _activityPublisher.EditMessageAsync(userData.ChatId, currentUser.State.MessageId, InlineKeyboardMarkup.Empty());

        var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

        var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

        var currentCity = "не задан";
        if (currentUser.CityId.HasValue)
        {
            var userCity = await _cityService.GetById(currentUser.CityId.Value);

            if (userCity is not null)
            {
                currentCity = userCity.Name;
            }
        }
        
        var message = await _activityPublisher.SendMessageAsync(
            userData.ChatId,
            CreateResponseMessage(currentCity), 
            null, 
            Keyboards.GetDefaultSettingsKeyboard(mskId, spbId, false));
         
        currentUser.State.MessageId = message.MessageId;
        
        currentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
        
        _userService.UpdateUser(currentUser);
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