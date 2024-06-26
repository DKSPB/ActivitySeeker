using System.Globalization;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class UserSetFromDateHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;

    public UserSetFromDateHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        var currentUser = _userService.GetUserById(message.From.Id);

        var fromDateText = update.Message.Text;
        var format = "dd.MM.yyyy";
        var format1 = "dd.MM.yyyy HH:mm";
        
        var result = ParseDate(fromDateText, format, out var fromDate) ? true 
            : ParseDate(fromDateText, format1, out fromDate);
        
        if (result)
        {
            currentUser.ActivityRequest.SearchFrom = fromDate;
            
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.MessageId = feedbackMessage.MessageId;
            currentUser.State = StatesEnum.PeriodToDate;
            _userService.CreateOrUpdateUser(currentUser);
        }
        else
        {
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введёная дата не соответствует форматам:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.MessageId = feedbackMessage.MessageId;
            _userService.CreateOrUpdateUser(currentUser);
        }
    }

    bool ParseDate(string fromDateText, string format, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }
}