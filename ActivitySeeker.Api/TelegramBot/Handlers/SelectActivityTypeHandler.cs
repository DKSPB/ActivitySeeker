using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
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
        CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        var activityTypes = ActivityService.GetActivityTypes();
        activityTypes.Insert(0, new ActivityType{Id = Guid.Empty, TypeName = "Все виды активностей"});
        
        return Keyboards.GetActivityTypesKeyboard(activityTypes);
    }
}