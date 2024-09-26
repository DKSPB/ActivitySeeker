using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Services;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SelectListOfferType)]
public class SelectListOfferType: AbstractHandler
{
    public SelectListOfferType(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : base(botClient, userService, activityService)
    {
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var selectedActivityTypeId = callbackQuery.Data;

        if (string.IsNullOrEmpty(selectedActivityTypeId))
        {
            throw new ArgumentNullException("");
        }

        var selectedActivityType = ActivityService.GetActivityType(Guid.Parse(selectedActivityTypeId));

        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = selectedActivityType.Id,
                OfferState = OffersEnum.NotOffered
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = selectedActivityType.Id;
        }
        
        ResponseMessageText = "Выбери способ добавления предложки";
        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return Keyboards.GetOfferKeyboard();
    }

}