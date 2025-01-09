using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityPeriodChapter)]
public class SelectActivityPeriodHandler : AbstractHandler
{
    public SelectActivityPeriodHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.ActivityPeriodChapter;
        
        ResponseMessageText = "Выбери период проведения активности:";
        Keyboard = Keyboards.GetPeriodActivityKeyboard();

        return Task.CompletedTask;
    }
}