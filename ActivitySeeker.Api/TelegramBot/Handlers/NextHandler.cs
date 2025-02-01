using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.NextActivity)]
public class NextHandler: AbstractHandler
{
    private ActivityTelegramDto? NextNode { get; set; }

    private readonly ActivityPublisher _activityPublisher;

    public NextHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.ActivityResult.Count > 0)
        {
            var currentActivity = CurrentUser.ActivityResult.First(x => x.Selected);

            var nextListNode = CurrentUser.ActivityResult.Find(currentActivity)?.Next;
           
            if (nextListNode is not null)
            {
                bool nodeIsLast = nextListNode.List?.Last?.Equals(nextListNode) ?? true;

                NextNode = nextListNode.Value;
                currentActivity.Selected = false;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);
                NextNode.Selected = true;

                Response.Text = NextNode.GetActivityDescription().ToString();
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard(true, !nodeIsLast);
                Response.Image = NextNode.Image;
            }
            else
            {
                NextNode = currentActivity;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);

                Response.Text = NextNode.GetActivityDescription().ToString();
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard(true, false);
                Response.Image = NextNode.Image;
            }
        }
        else
        {
            const string activitiesNotFoundMessage = "По вашему запросу активностей не найдено";
            Response.Text = activitiesNotFoundMessage;
            Response.Keyboard = Keyboards.GetToMainMenuKeyboard();
        }
    }
}