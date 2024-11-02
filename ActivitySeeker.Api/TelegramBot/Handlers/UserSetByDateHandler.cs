using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodToDate)]
public class UserSetByDateHandler: IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public UserSetByDateHandler(IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, UserMessage userData)
    {
        var byDateText = userData.Data;

        var result = DateParser.ParseDate(byDateText, out var byDate) || 
                     DateParser.ParseDateTime(byDateText, out byDate);
        
        if (result)
        {
            currentUser.State.SearchTo = byDate;

            var feedbackMessage = await _activityPublisher.SendMessageAsync(userData.ChatId,
                currentUser.State.ToString(), null, Keyboards.GetMainMenuKeyboard());
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var msgText = $"Введёная дата не соответствует форматам:" +
                          $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                          $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            var feedbackMessage = await _activityPublisher.SendMessageAsync(
                userData.ChatId,
                msgText
                );
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
}