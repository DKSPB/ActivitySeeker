using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Result)]
public class SearchResultHandler : AbstractHandler
{
    private ActivityTelegramDto? CurrentActivity { get; set; }

    public SearchResultHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        : base(userService, activityService, activityPublisher)
    { }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var activities = ActivityService.GetActivitiesLinkedList(CurrentUser);

        if (activities.Count > 0)
        {
            CurrentActivity = activities.First();
            CurrentActivity.Image = await ActivityService.GetImage(CurrentActivity.Id);
            CurrentActivity.Selected = true;
            CurrentUser.ActivityResult = activities;

            Response.Text = CurrentActivity.GetActivityDescription().ToString();
            Response.Keyboard = Keyboards.GetActivityPaginationKeyboard(false);
            Response.Image = CurrentActivity.Image;
        }
        else
        {
            Response.Text = "По вашему запросу активностей не найдено";
            Response.Keyboard = Keyboards.GetToMainMenuKeyboard();
        }
    }
}