using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : AbstractHandler
{
    public ConfirmOfferHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : base(botClient, userService, activityService)
    {
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        CurrentUser.Offer.OfferState = OffersEnum.Offered;
        
        ResponseMessageText = $"Предложенная активность отправлена на модерацию " +
                              $"\n{CurrentUser.State.ToString()}";

        CurrentUser.Offer = null;

        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}