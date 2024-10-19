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
        IActivityService activityService, ActivityPublisher activityPublisher) :
        base(botClient, userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        var selectedActivityTypeId = callbackQuery.Data;

        if (Guid.Parse(callbackQuery.Data) == Guid.Empty)
        {
            CurrentUser.State.ActivityType = new();
        }
        else
        {
            var selectedActivityType = ActivityService.GetActivityType(Guid.Parse(selectedActivityTypeId));
            CurrentUser.State.ActivityType.Id = selectedActivityType.Id;
            CurrentUser.State.ActivityType.TypeName = selectedActivityType.TypeName;
        }

        ResponseMessageText = CurrentUser.State.ToString();
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}