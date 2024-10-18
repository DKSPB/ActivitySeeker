using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    //private readonly ActivityPublisher _activityPublisher;
    public AddOfferDescriptionHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher)
        : base(botClient, userService, activityService, activityPublisher)
    {
        //_activityPublisher = activityPublisher;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        
        ResponseMessageText = $"Заполни описание события";

        var selectedActivityTypeId = callbackQuery.Data;

        if (string.IsNullOrEmpty(selectedActivityTypeId))
        {
            throw new ArgumentNullException($"Отсутствует события с идентификатором {selectedActivityTypeId}");
        }

        var selectedActivityType = ActivityService.GetActivityType(Guid.Parse(selectedActivityTypeId));

        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = selectedActivityType.Id,
                LinkOrDescription = "offer_in_create_process",
                OfferState = false
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = selectedActivityType.Id;
        }
        
        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return InlineKeyboardMarkup.Empty();
    }
}