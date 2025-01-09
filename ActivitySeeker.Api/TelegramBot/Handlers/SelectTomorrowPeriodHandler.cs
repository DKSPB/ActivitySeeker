using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.TomorrowPeriod)]
public class SelectTomorrowPeriodHandler: AbstractHandler
{
    public SelectTomorrowPeriodHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.SearchFrom = DateTime.Now.AddDays(1).Date;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(2).Date;
        
        ResponseMessageText = CurrentUser.State.ToString();
        Keyboard = Keyboards.GetMainMenuKeyboard();
        
        return Task.CompletedTask;
    }
}