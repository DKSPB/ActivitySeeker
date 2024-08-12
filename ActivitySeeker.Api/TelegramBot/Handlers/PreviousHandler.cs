using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PreviousActivity)]
public class PreviousHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    private ActivityTelegramDto? PreviousNode { get; set; }
    
    public PreviousHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : 
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
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
                ResponseMessageText = PreviousNode.Name;
            }
            else
            {
                PreviousNode = currentActivity;
                PreviousNode.Image = await ActivityService.GetImage(PreviousNode.Id);
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
        if (PreviousNode is null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (PreviousNode.Image is null && PreviousNode.Link is not null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: PreviousNode.Link,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (PreviousNode.Image is not null && PreviousNode.Link is null)
        {
            var caption = PreviousNode.ToString();
            
            if (caption.Length <= 1024)
            {
                return await BotClient.SendPhotoAsync(chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(PreviousNode.Image)),
                    caption: PreviousNode.ToString(),
                    replyMarkup: GetKeyboard(), 
                    cancellationToken: cancellationToken);
            }
            
            await BotClient.SendPhotoAsync(chatId: chatId,
                photo: new InputFileStream(new MemoryStream(PreviousNode.Image)),
                cancellationToken: cancellationToken);
        
            return await BotClient.SendTextMessageAsync(chatId: chatId,
                text: PreviousNode.ToString(),
                replyMarkup: GetKeyboard(), 
                cancellationToken: cancellationToken);
        }
        
        return await BotClient.SendTextMessageAsync(
            chatId,
            text: PreviousNode.ToString(),
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