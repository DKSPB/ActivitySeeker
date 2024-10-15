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

    public NextHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) : 
        base(botClient, userService, activityService)
    {
        _activityPublisher = activityPublisher;
        ResponseMessageText = MessageText;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
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

    protected override async Task<Message> SendMessageAsync(long chatId, CancellationToken cancellationToken)
    {
        if (NextNode is null)
        {
            /*return await BotClient.SendTextMessageAsync(
                chatId,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);*/
            return await _activityPublisher.PublishActivity(chatId, ResponseMessageText, null, GetKeyboard(), cancellationToken);
        }

        return await _activityPublisher.PublishActivity(chatId, NextNode.LinkOrDescription, NextNode.Image, GetKeyboard(), cancellationToken);

        /*if (NextNode.Image is null && NextNode.Link is not null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: NextNode.Link,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (NextNode.Image is not null && NextNode.LinkOrDescription is not null && NextNode.Link is null)
        {
            var caption = NextNode.LinkOrDescription;
            
            if (caption.Length <= 1024)
            {
                return await BotClient.SendPhotoAsync(chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(NextNode.Image)),
                    caption: NextNode.LinkOrDescription,
                    replyMarkup: GetKeyboard(), 
                    cancellationToken: cancellationToken);
            }
            
            await BotClient.SendPhotoAsync(chatId: chatId,
                photo: new InputFileStream(new MemoryStream(NextNode.Image)),
                cancellationToken: cancellationToken);
            
            return await BotClient.SendTextMessageAsync(chatId: chatId,
                text: NextNode.LinkOrDescription,
                replyMarkup: GetKeyboard(), 
                cancellationToken: cancellationToken);
        }
        
        return await BotClient.SendTextMessageAsync(chatId: chatId,
            text: NextNode.LinkOrDescription,
            replyMarkup: GetKeyboard(), 
            cancellationToken: cancellationToken);*/
    }
    
    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetActivityPaginationKeyboard();
    }

    protected override async Task EditPreviousMessage(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await BotClient.DeleteMessageAsync(
            callbackQuery.Message.Chat.Id,
            CurrentUser.State.MessageId,
            cancellationToken);
    }
}