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
        : base(botClient, userService, activityService, activityPublisher)
    {
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery)
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

    protected override async Task<Message> SendMessageAsync(long chatId)
    {
        if (CurrentActivity is null)
        {
            return await _activityPublisher.PublishActivity(chatId, ResponseMessageText, null, GetKeyboard());
        }

        return await _activityPublisher.PublishActivity(chatId, CurrentActivity.LinkOrDescription, CurrentActivity.Image, GetKeyboard());
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