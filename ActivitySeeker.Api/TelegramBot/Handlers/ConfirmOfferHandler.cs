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
    private readonly ActivityPublisher _activityPublisher;
    public ConfirmOfferHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, NotificationAdminHub adminHub)
        : base(userService, activityService, activityPublisher)
    {
        _adminHub = adminHub;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        await _adminHub.Send(JsonConvert.SerializeObject(CurrentUser.Offer));

        await _activityPublisher.SendMessageAsync(
            callbackQuery.Message.Chat.Id, 
            "Поздравляю! Твоя активность создана. Она будет опубликована после проверки", 
            null, 
            Keyboards.GetEmptyKeyboard());
            
        ResponseMessageText = $"{CurrentUser.State}";

        CurrentUser.Offer = null;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}