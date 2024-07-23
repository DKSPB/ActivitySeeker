using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityTypeChapter)]
public class SelectActivityTypeHandler: AbstractHandler
{
    public SelectActivityTypeHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService):
        base(botClient, userService, activityService)
    {    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ResponseMessageText = "Выбери тип активности:";
        CurrentUser.State = StatesEnum.ListOfActivities;
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        var activityTypes = ActivityService.GetActivityTypes();
        return Keyboards.GetActivityTypesKeyboard(activityTypes);
    }
}