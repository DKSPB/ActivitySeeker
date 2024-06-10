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
        CurrentUser.ActivityRequest.SearchFrom = DateTime.Now.AddDays(1).Date;
        CurrentUser.ActivityRequest.SearchTo = DateTime.Now.AddDays(2).Date;
        ResponseMessageText = CurrentUser.ActivityRequest.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}