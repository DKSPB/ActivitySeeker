using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityPeriodChapter)]
public class SelectActivityPeriodHandler: AbstractHandler
{
    private const string MessageText = "Выбери период проведения активности:";
    
    public SelectActivityPeriodHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.ActivityPeriodChapter;

        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetPeriodActivityKeyboard();
    }
}