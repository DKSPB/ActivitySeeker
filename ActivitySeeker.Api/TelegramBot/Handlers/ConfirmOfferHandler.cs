using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Notification;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : AbstractHandler
{
    private readonly NotificationAdminHub _adminHub;
    private readonly ActivityPublisher _activityPublisher;
    public ConfirmOfferHandler(IUserService userService, IActivityService activityService, 
        ActivityPublisher activityPublisher, NotificationAdminHub adminHub)
        : base(userService, activityService, activityPublisher)
    {
        _adminHub = adminHub;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        await _adminHub.Send(JsonConvert.SerializeObject(CurrentUser.Offer));

        var offerConfirmResponse = new ResponseMessage
        {
            Text = "Поздравляю! Твоя активность создана. Она будет опубликована после проверки",
            Keyboard = Keyboards.GetEmptyKeyboard()
        };
        
        await _activityPublisher.SendMessageAsync(userData.ChatId, offerConfirmResponse);
            
        Response.Text = $"{CurrentUser.State}";
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();

        CurrentUser.Offer = null;
    }
}