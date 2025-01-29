using ActivitySeeker.Api.Models;
using Telegram.Bot.Types;
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
                PreviousNode = previousListNode.Value;
                currentActivity.Selected = false;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
                PreviousNode.Selected = true;

                Response.Text = PreviousNode.GetActivityDescription().ToString();
                Response.Image = PreviousNode.Image;
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard();
            }
            else
            {
                PreviousNode = currentActivity;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
                
                const string messageText = "Найденные активности:";
                Response.Text = messageText;
                Response.Keyboard = Keyboards.GetActivityPaginationKeyboard();
            }
        }
        else
        {
            const string activitiesNotFoundMessage = "По вашему запросу активностей не найдено";
            Response.Text = activitiesNotFoundMessage;
        }
    }

    /*protected override async Task<Message> SendMessageAsync(long chatId)
    {
        if (PreviousNode is null)
        {
            return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, null, Keyboards.GetActivityPaginationKeyboard());
        }

        return await _activityPublisher.SendMessageAsync(chatId, PreviousNode.GetActivityDescription().ToString(), PreviousNode.Image, Keyboards.GetActivityPaginationKeyboard());

    }*/
    
    /*protected override async Task EditPreviousMessage(ChatId chatId)
    {
        await _activityPublisher.DeleteMessage(chatId, CurrentUser.State.MessageId);
    }*/
}