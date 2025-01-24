using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

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
                NextNode = nextListNode.Value;
                currentActivity.Selected = false;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);
                NextNode.Selected = true;

                Response.Text = NextNode.GetActivityDescription().ToString();
                Response.Image = NextNode.Image;
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard();
            }
            else
            {
                NextNode = currentActivity;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);
                
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard();
            }
        }
        else
        {
            const string activitiesNotFoundMessage = "По вашему запросу активностей не найдено";
            Response.Text = activitiesNotFoundMessage;
        }
    }

    protected override async Task EditPreviousMessage(ChatId chatId)
    {
        await _activityPublisher.DeleteMessage(chatId, CurrentUser.State.MessageId);
    }
}