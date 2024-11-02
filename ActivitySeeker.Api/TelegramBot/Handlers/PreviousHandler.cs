using ActivitySeeker.Api.Models;
using Telegram.Bot.Types;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;


namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PreviousActivity)]
public class PreviousHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    private ActivityTelegramDto? PreviousNode { get; set; }

    private readonly ActivityPublisher _activityPublisher;
    
    public PreviousHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
        ResponseMessageText = MessageText;
    }

    protected override async Task ActionsAsync(UserMessage userData)
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
            }
            else
            {
                PreviousNode = currentActivity;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
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
        if (PreviousNode is null)
        {
            return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, null, GetKeyboard());
        }

        return await _activityPublisher.SendMessageAsync(chatId, PreviousNode.LinkOrDescription, PreviousNode.Image, GetKeyboard());

    }
    
    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetActivityPaginationKeyboard();
    }

    protected override async Task EditPreviousMessage(ChatId chatId)
    {
        await _activityPublisher.DeleteMessage(chatId, CurrentUser.State.MessageId);
    }
}