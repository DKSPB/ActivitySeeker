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

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        CurrentUser.Offer.OfferState = OffersEnum.Offered;
        
        await BotClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            text:"Поздравляю! Твоя активность создана. Она будет опубликована после проверки",
            cancellationToken: cancellationToken);
            
        ResponseMessageText = $"{CurrentUser.State}";

        CurrentUser.Offer = null;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}