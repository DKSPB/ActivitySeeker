using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    public AddOfferDescriptionHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher)
        : base(botClient, userService, activityService, activityPublisher)
    { }

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

        CreateOfferIfNotExists(selectedActivityType.Id);
        
        return Task.CompletedTask;
    }

    private void CreateOfferIfNotExists(Guid activityTypeId)
    {
        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = activityTypeId,
                LinkOrDescription = string.Empty,
                OfferState = false
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = activityTypeId;
        }
    }
}