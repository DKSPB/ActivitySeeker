using System.Globalization;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodFromDate)]
public class UserSetFromDateHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;

    public UserSetFromDateHandler(ITelegramBotClient botClient, IUserService userService, ActivityPublisher activityPublisher)
    {
        _botClient = botClient;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var message = update.Message;

        var fromDateText = update.Message.Text;
        var format = "dd.MM.yyyy";
        var format1 = "dd.MM.yyyy HH:mm";
        
        var result = ParseDate(fromDateText, format, out var fromDate) ? true 
            : ParseDate(fromDateText, format1, out fromDate);
        
        if (result)
        {

            var msgText = $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            currentUser.State.SearchFrom = fromDate;

            var feedbackMessage = await _activityPublisher.PublishActivity(message.Chat.Id, msgText, null, InlineKeyboardMarkup.Empty());
                
                
                
                /*_botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: msgText,
                cancellationToken: cancellationToken);*/
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            currentUser.State.StateNumber = StatesEnum.PeriodToDate;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var msgText = $"Введёная дата не соответствует форматам:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            var feedbackMessage = await _activityPublisher.PublishActivity(message.Chat.Id, msgText, null, InlineKeyboardMarkup.Empty());
                
                /*await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: msgText,
                cancellationToken: cancellationToken);*/
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }

    bool ParseDate(string fromDateText, string format, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }
}