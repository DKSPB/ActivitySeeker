using System.Globalization;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodFromDate)]
public class UserSetFromDateHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;

    public UserSetFromDateHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        var fromDateText = update.Message.Text;
        var format = "dd.MM.yyyy";
        var format1 = "dd.MM.yyyy HH:mm";
        
        var result = ParseDate(fromDateText, format, out var fromDate) ? true 
            : ParseDate(fromDateText, format1, out fromDate);
        
        if (result)
        {
            currentUser.State.SearchFrom = fromDate;
            
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            currentUser.State.StateNumber = StatesEnum.PeriodToDate;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введёная дата не соответствует форматам:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }

    bool ParseDate(string fromDateText, string format, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }
}