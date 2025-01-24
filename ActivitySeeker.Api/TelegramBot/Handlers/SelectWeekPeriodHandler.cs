using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.WeekPeriod)]
public class SelectWeekPeriodHandler: AbstractHandler
{
    public SelectWeekPeriodHandler(IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.SearchFrom = DateTime.Now;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(7).Date;
        
        Response.Text = CurrentUser.State.ToString();
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();
        
        return Task.CompletedTask;
    }
}