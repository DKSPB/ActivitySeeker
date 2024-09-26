using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;
    
    public ConfirmOfferHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
    {
        _botClient = botClient;
        _userService = userService;
        _activityService = activityService;
    }
    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        
        var activityId = currentUser.OfferId ?? Guid.Empty;
        
        currentUser.State.StateNumber = StatesEnum.Start;
        
        var offer = await _activityService.GetActivityAsync(activityId);
        offer.OfferState = (OffersEnum)1;
        
        var feedbackMessage = await _botClient.SendTextMessageAsync(
            message.Chat.Id,
            text: $"Предложка успешно отправлена на модерацию!",
            cancellationToken: cancellationToken);

        await _activityService.UpdateActivity(offer);
            
        currentUser.State.MessageId = feedbackMessage.MessageId;
        _userService.UpdateUser(currentUser);
    }
}