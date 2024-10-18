using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.TomorrowPeriod)]
public class SelectTomorrowPeriodHandler: AbstractHandler
{
    public SelectTomorrowPeriodHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(botClient, userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        CurrentUser.State.SearchFrom = DateTime.Now.AddDays(1).Date;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(2).Date;
        ResponseMessageText = CurrentUser.State.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}