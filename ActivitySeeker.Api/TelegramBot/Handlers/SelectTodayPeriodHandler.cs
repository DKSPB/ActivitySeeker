using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.TodayPeriod)]
public class SelectTodayPeriodHandler: AbstractHandler
{
    public SelectTodayPeriodHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.SearchFrom = DateTime.Now;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(1).Date;
        
        Response.Text = CurrentUser.State.ToString();
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();

        return Task.CompletedTask;
    }
}