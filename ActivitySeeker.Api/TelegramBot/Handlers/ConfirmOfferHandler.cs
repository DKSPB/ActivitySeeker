using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Notification;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : AbstractHandler
{
    private readonly NotificationAdminHub _adminHub;
    public ConfirmOfferHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, NotificationAdminHub adminHub)
        : base(botClient, userService, activityService)
    {
        _adminHub = adminHub;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        await _adminHub.Send(JsonConvert.SerializeObject(CurrentUser.Offer));
        
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