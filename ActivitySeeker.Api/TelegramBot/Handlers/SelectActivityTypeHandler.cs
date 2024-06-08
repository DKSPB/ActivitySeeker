using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class SelectActivityTypeHandler: AbstractHandler
{
    public SelectActivityTypeHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService):
        base(botClient, userService, activityService)
    {    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ResponseMessageText = CurrentUser.ActivityRequest.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        var activityTypes = ActivityService.GetActivityTypes();
        return Keyboards.GetActivityTypesKeyboard(activityTypes);
    }
}