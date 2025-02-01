using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PreviousActivity)]
public class PreviousHandler: AbstractHandler
{
    private ActivityTelegramDto? PreviousNode { get; set; }

    private readonly ActivityPublisher _activityPublisher;
    
    public PreviousHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.ActivityResult.Count > 0)
        {
            var currentActivity = CurrentUser.ActivityResult.First(x => x.Selected);
            
            var previousListNode = CurrentUser.ActivityResult.Find(currentActivity)?.Previous;

            if (previousListNode is not null)
            {
                bool nodeIsFirst = previousListNode.List?.First?.Equals(previousListNode) ?? true;

                PreviousNode = previousListNode.Value;
                currentActivity.Selected = false;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
                PreviousNode.Selected = true;

                Response.Text = PreviousNode.GetActivityDescription().ToString();
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard(!nodeIsFirst);
                Response.Image = PreviousNode.Image;
            }
            else
            {
                PreviousNode = currentActivity;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
                
                Response.Text = PreviousNode.GetActivityDescription().ToString();
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard(false);
                Response.Image = PreviousNode.Image;
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