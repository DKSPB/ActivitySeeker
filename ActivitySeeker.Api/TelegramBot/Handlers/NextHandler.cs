using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.NextActivity)]
public class NextHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    private ActivityTelegramDto? NextNode { get; set; }

    private readonly ActivityPublisher _activityPublisher;

    public NextHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
        ResponseMessageText = MessageText;
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
            }
            else
            {
                NextNode = currentActivity;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);
            }
        }
        else
        {
            const string activitiesNotFoundMessage = "По вашему запросу активностей не найдено";
            ResponseMessageText = string.Concat(ResponseMessageText, $"\n {activitiesNotFoundMessage}");
        }
    }

    protected override async Task<Message> SendMessageAsync(long chatId)
    {
        if (NextNode is null)
        {
            return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, null, Keyboards.GetActivityPaginationKeyboard());
        }

        return await _activityPublisher.SendMessageAsync(chatId, NextNode.GetActivityDescription().ToString(), NextNode.Image, Keyboards.GetActivityPaginationKeyboard());
    }

    protected override async Task EditPreviousMessage(ChatId chatId)
    {
        await _activityPublisher.DeleteMessage(chatId, CurrentUser.State.MessageId);
    }
}