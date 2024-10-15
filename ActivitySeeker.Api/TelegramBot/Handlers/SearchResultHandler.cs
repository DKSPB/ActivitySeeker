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

    private readonly ActivityPublisher _activityPublisher;

    public SearchResultHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        : base(botClient, userService, activityService)
    {
        _activityPublisher = activityPublisher;
    }

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
            /*return await BotClient.SendTextMessageAsync(
                chatId,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);*/
            return await _activityPublisher.PublishActivity(chatId, ResponseMessageText, null, GetKeyboard(), cancellationToken);
        }

        return await _activityPublisher.PublishActivity(chatId, CurrentActivity.LinkOrDescription, CurrentActivity.Image, GetKeyboard(), cancellationToken);


        /*if (CurrentActivity.Image is null && CurrentActivity.Link is not null)
        {
            return await BotClient.SendTextMessageAsync(
                chatId,
                text: CurrentActivity.Link,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);
        }

        if (CurrentActivity.Image is not null && CurrentActivity.LinkOrDescription is not null && CurrentActivity.Link is null)
        {
            var caption = CurrentActivity.LinkOrDescription;
            
            if (caption.Length <= 1024)
            {
                return await BotClient.SendPhotoAsync(chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(CurrentActivity.Image)),
                    caption: CurrentActivity.LinkOrDescription,
                    replyMarkup: GetKeyboard(), 
                    cancellationToken: cancellationToken);
            }

            await BotClient.SendPhotoAsync(chatId: chatId,
                photo: new InputFileStream(new MemoryStream(CurrentActivity.Image)),
                cancellationToken: cancellationToken);
    
            return await BotClient.SendTextMessageAsync(chatId: chatId,
                text: CurrentActivity.LinkOrDescription,
                replyMarkup: GetKeyboard(), 
                cancellationToken: cancellationToken);

        }
        
        return await BotClient.SendTextMessageAsync(chatId: chatId,
            text: CurrentActivity.LinkOrDescription,
            replyMarkup: GetKeyboard(), 
            cancellationToken: cancellationToken);*/
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