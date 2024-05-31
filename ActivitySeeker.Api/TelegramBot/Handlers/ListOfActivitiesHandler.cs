using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class ListOfActivitiesHandler: AbstractHandler
{
    private const string MessageText = "Выбери тип активности и время проведения";

    public ListOfActivitiesHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService):
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var selectedActivityTypeId = callbackQuery.Data.Split('/')[1];
        CurrentUser.ActivityRequest.ActivityTypeId = Guid.Parse(selectedActivityTypeId);
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}