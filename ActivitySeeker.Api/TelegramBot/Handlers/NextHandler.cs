using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.NextActivity)]
public class NextHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    private ActivityTelegramDto? NextNode { get; set; }

    private readonly ActivityPublisher _activityPublisher;

    public NextHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher): 
        base(botClient, userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
        ResponseMessageText = MessageText;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery)
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
            return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, null, GetKeyboard());
        }

        return await _activityPublisher.SendMessageAsync(chatId, NextNode.LinkOrDescription, NextNode.Image, GetKeyboard());

    }
    
    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetActivityPaginationKeyboard();
    }

    protected override async Task EditPreviousMessage(CallbackQuery callbackQuery)
    {
        await _activityPublisher.DeleteMessage(callbackQuery.Message.Chat.Id, CurrentUser.State.MessageId);
    }
}