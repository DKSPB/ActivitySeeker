using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SetDefaultSettings)]
public class SetDefaultSettingsHandler: IHandler
{
    private const string MessageText = $"Перед началом использования бота задайте Ваш Город." +
                                   $"\nЕсли Ваш город не Москва и Санкт-Петербург, ввведите его название, как текст сообщения" +
                                   $"\nВы всегда сможете изменить эту настройку в разделе:" +
                                   $"\nМеню > Настройки";

    private readonly ICityService _cityService;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public SetDefaultSettingsHandler(ICityService cityService, IUserService userService, ActivityPublisher activityPublisher)
    {
        _cityService = cityService;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, UserMessage userData)
    {
        var mskId = (await _cityService.GetCitiesByName("Москва")).First().Id;

        var spbId = (await _cityService.GetCitiesByName("Санкт-Петербург")).First().Id;

        var message = await _activityPublisher.SendMessageAsync(
            userData.ChatId,
            MessageText, 
            null, 
            Keyboards.GetDefaultSettingsKeyboard(mskId, spbId));
         
        currentUser.State.MessageId = message.MessageId;
        
        currentUser.State.StateNumber = StatesEnum.SaveDefaultSettings;
        
        _userService.UpdateUser(currentUser);
    }
}