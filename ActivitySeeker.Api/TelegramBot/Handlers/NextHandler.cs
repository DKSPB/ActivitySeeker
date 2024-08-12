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
    
    public NextHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : 
        base(botClient, userService, activityService)
    {
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
                ResponseMessageText = NextNode.Name;
            }
            else
            {
                NextNode = currentActivity;
                NextNode.Image = await ActivityService.GetImage(NextNode.Id);
                ResponseMessageText = currentActivity.Name;
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
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (NextNode.Image is null && NextNode.Link is not null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: NextNode.Link,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (NextNode.Image is not null && NextNode.Link is null)
        {
            var caption = NextNode.ToString();
            
            if (caption.Length <= 1024)
            {
                return await BotClient.SendPhotoAsync(chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(NextNode.Image)),
                    caption: NextNode.ToString(),
                    replyMarkup: GetKeyboard(), 
                    cancellationToken: cancellationToken);
            }
            
            await BotClient.SendPhotoAsync(chatId: chatId,
                photo: new InputFileStream(new MemoryStream(NextNode.Image)),
                cancellationToken: cancellationToken);
            
            return await BotClient.SendTextMessageAsync(chatId: chatId,
                text: NextNode.ToString(),
                replyMarkup: GetKeyboard(), 
                cancellationToken: cancellationToken);
        }
        
        return await BotClient.SendTextMessageAsync(chatId: chatId,
            text: NextNode.ToString(),
            replyMarkup: GetKeyboard(), 
            cancellationToken: cancellationToken);
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