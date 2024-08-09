using Telegram.Bot;
using Telegram.Bot.Types;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Result)]
public class SearchResultHandler: AbstractHandler
{
    private ActivityTelegramDto? CurrentActivity { get; set; }

    public SearchResultHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
        : base(botClient, userService, activityService)
    { }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var activities = ActivityService.GetActivitiesLinkedList(CurrentUser.State);

        if (activities.Count > 0)
        {
            ResponseMessageText = $"Найдено активностей: {activities.Count}";
            CurrentActivity = activities.First();
            CurrentActivity.Image = await ActivityService.GetImage(CurrentActivity.Id);
            CurrentActivity.Selected = true;
            CurrentUser.ActivityResult = activities;
        }
        else
        {
            ResponseMessageText = "По вашему запросу активностей не найдено";
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
        if (CurrentActivity is null)
        {
            return Keyboards.GetToMainMenuKeyboard();
        }
        return Keyboards.GetActivityPaginationKeyboard();
    }
}