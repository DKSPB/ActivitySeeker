using System.Globalization;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDate)]
public class SaveOfferDateHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;

    public SaveOfferDateHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
    {
        _botClient = botClient;
        _userService = userService;
        _activityService = activityService;
    }

    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;

        var byDateText = update.Message.Text;
        var format = "dd.MM.yyyy HH:mm";

        var result = ParseDate(byDateText, format, out var byDate);

        var activityId = currentUser.OfferId ?? Guid.Empty;

        if (result)
        {
            currentUser.State.StateNumber = StatesEnum.ConfirmOffer;
            
            var offer = await _activityService.GetActivityAsync(activityId);
            offer.StartDate = byDate;

            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Подтвердите предлагаемое мероприятие:" +
                      $"\nмероприятие",
                replyMarkup: Keyboards.ConfirmOffer(),
                cancellationToken: cancellationToken);
            
            await _activityService.UpdateActivity(offer);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введёная дата не соответствует формату:" +
                      $"\n(дд.мм.гггг чч.мм)" +
                      $"\nпример: {DateTime.Now:dd.MM.yyyy HH:mm}",
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