using System.Globalization;
using System.Text;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDate)]
public class SaveOfferDateHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;
    private readonly ActivityPublisher _activityPublisher;
    private readonly ILogger<SaveOfferDateHandler> _logger;

    public SaveOfferDateHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, ILogger<SaveOfferDateHandler> logger)
    {
        _logger = logger;
        _botClient = botClient;
        _userService = userService;
        _activityService = activityService;
        _activityPublisher = activityPublisher;
    }

    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var message = update.Message;

        if (message is null)
        {
            var errorMessage = "Объект update.Message is null";
            _logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }

        if (message.Text is null)
        {
            var errorMessage = "Объект update.Message.Text is null";
            _logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }
        
        if (currentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности, объект offer is null");
        }
        
        var dateTimeFormat = "dd.MM.yyyy HH:mm";
        var startActivityDateText = message.Text;

        var parsingDateResult = ParseDate(startActivityDateText, dateTimeFormat, out var startActivityDate);

        if (parsingDateResult)
        {
            currentUser.Offer.StartDate = startActivityDate;

            var feedbackMessage = await _activityPublisher.PublishActivity(message.Chat.Id, GetFullOfferContent(currentUser.Offer), null, Keyboards.ConfirmOffer());
                
                
                /*_botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: GetFullOfferContent(currentUser.Offer),
                replyMarkup: Keyboards.ConfirmOffer(),
                cancellationToken: cancellationToken);*/
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var feedbackMessage = await _activityPublisher.PublishActivity(
                message.Chat.Id, 
                $"Введёная дата не соответствует формату:" +
                $"\n(дд.мм.гггг чч:мм)" +
                $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}",
                null,
                InlineKeyboardMarkup.Empty());
                
                
               /* _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введёная дата не соответствует формату:" +
                      $"\n(дд.мм.гггг чч:мм)" +
                      $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);*/
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
    
    private bool ParseDate(string fromDateText, string format, out DateTime fromDate)
    {
        return DateTime.TryParseExact(fromDateText, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDate);
    }

    private string GetFullOfferContent(ActivityDto offer)
    {
        StringBuilder builder = new();

        builder.AppendLine("Эта активность будет предложена для публикации.");
        builder.AppendLine("Убедись, что данные заполнены корректно ");
        builder.AppendLine("Тип активности:");
        builder.AppendLine(_activityService.GetActivityType(offer.ActivityTypeId).TypeName);
        builder.AppendLine("Дата и время начала:");
        builder.AppendLine(offer.StartDate.ToString("dd.MM.yyyy HH:mm"));
        builder.AppendLine("Описание активности:");
        builder.AppendLine(offer.LinkOrDescription);

        return builder.ToString();
    }
}