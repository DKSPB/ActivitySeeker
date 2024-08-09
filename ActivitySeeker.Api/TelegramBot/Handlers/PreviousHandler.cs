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
    
    private ActivityTelegramDto? CurrentActivity { get; set; }
    
    public PreviousHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : 
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        if (CurrentUser.ActivityResult.Count > 0)
        {
            CurrentActivity = CurrentUser.ActivityResult.First(x => x.Selected);
            
            var previousNode = CurrentUser.ActivityResult.Find(CurrentActivity)?.Previous;
            if (previousNode is not null)
            {
                CurrentActivity.Selected = false;
                CurrentActivity.Image = await ActivityService.GetImage(CurrentActivity.Id);
                previousNode.Value.Selected = true;
                ResponseMessageText = previousNode.Value.Name;
            }
            else
            {
                ResponseMessageText = CurrentActivity.Name;
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
        if (CurrentActivity is null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        var caption = CurrentActivity.ToString();
        
        if (CurrentActivity.Image is null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: caption,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (caption.Length <= 1024)
        {
            return await BotClient.SendPhotoAsync(chatId: chatId,
                photo: new InputFileStream(new MemoryStream(CurrentActivity.Image)),
                caption: CurrentActivity.ToString(),
                replyMarkup: GetKeyboard(), 
                cancellationToken: cancellationToken);
        }

        await BotClient.SendPhotoAsync(chatId: chatId,
            photo: new InputFileStream(new MemoryStream(CurrentActivity.Image)),
            cancellationToken: cancellationToken);
        
        return await BotClient.SendTextMessageAsync(chatId: chatId,
            text: CurrentActivity.ToString(),
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