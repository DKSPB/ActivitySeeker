using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodFromDate)]
public class UserSetFromDateHandler: IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;

    public UserSetFromDateHandler(IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var message = update.Message;

        var fromDateText = update.Message.Text;

        var result = DateParser.ParseDate(fromDateText, out var fromDate) || 
                     DateParser.ParseDateTime(fromDateText, out fromDate);
        
        if (result)
        {
            var msgText = $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            currentUser.State.SearchFrom = fromDate;

            var feedbackMessage = await _activityPublisher.SendMessageAsync(message.Chat.Id, msgText);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            currentUser.State.StateNumber = StatesEnum.PeriodToDate;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var msgText = $"Введёная дата не соответствует форматам:" +
              $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
              $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            var feedbackMessage = await _activityPublisher.SendMessageAsync(message.Chat.Id, msgText);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
}