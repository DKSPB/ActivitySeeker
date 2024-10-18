using System.Globalization;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodToDate)]
public class UserSetByDateHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public UserSetByDateHandler(ITelegramBotClient botClient, IUserService userService, ActivityPublisher activityPublisher)
    {
        _botClient = botClient;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var message = update.Message;

        var byDateText = update.Message.Text;
        var format = "dd.MM.yyyy";
        var format1 = "dd.MM.yyyy HH:mm";
        
        var result = ParseDate(byDateText, format, out var byDate) ? true 
            : ParseDate(byDateText, format1, out byDate);
        
        if (result)
        {
            currentUser.State.SearchTo = byDate;

            var feedbackMessage = await _activityPublisher.PublishActivity(message.Chat.Id, currentUser.State.ToString(), null, Keyboards.GetMainMenuKeyboard());
                
                /*_botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: currentUser.State.ToString(),
                replyMarkup: Keyboards.GetMainMenuKeyboard(),
                cancellationToken: cancellationToken);*/
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var feedbackMessage = await _activityPublisher.PublishActivity(
                message.Chat.Id,
                $"Введёная дата не соответствует форматам:" +
                $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                null,
                InlineKeyboardMarkup.Empty());
                
                /*_botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введёная дата не соответствует форматам:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
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