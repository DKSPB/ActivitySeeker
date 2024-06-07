using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class SelectTomorrowPeriodHandler: AbstractHandler
{
    public SelectTomorrowPeriodHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) 
        : base(botClient, userService, activityService)
    { }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ResponseMessageText = CurrentUser.ActivityRequest.ToString();
        CurrentUser.ActivityRequest.SearchFrom = DateTime.Now.AddDays(1);
        CurrentUser.ActivityRequest.SearchTo = DateTime.Now.AddDays(1);
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}