using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ListOfActivities)]
public class ListOfActivitiesHandler: AbstractHandler
{
    public ListOfActivitiesHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService):
        base(botClient, userService, activityService)
    {    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var selectedActivityTypeId = callbackQuery.Data;

        if(selectedActivityTypeId is null )
        {
            throw new ArgumentNullException(nameof(selectedActivityTypeId));
        }

        if (string.IsNullOrEmpty(selectedActivityTypeId))
        {
            CurrentUser.ActivityRequest.ActivityTypeId = null;
            CurrentUser.ActivityRequest.ActivityType = "Все виды активности";
        }
        else
        {
            var selectedActivityType = ActivityService.FindActivityType(Guid.Parse(selectedActivityTypeId));
            CurrentUser.ActivityRequest.ActivityTypeId = selectedActivityType.Id;
            CurrentUser.ActivityRequest.ActivityType = selectedActivityType.TypeName;
        }

        ResponseMessageText = CurrentUser.ActivityRequest.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}