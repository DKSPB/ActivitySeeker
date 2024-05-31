using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class SearchResultHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";

    public SearchResultHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
        : base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        var activities = ActivityService.GetActivities(new ActivityRequest());

        if (activities.Count > 0)
        {
            var currentActivity = activities.First();
            currentActivity.Selected = true;
            CurrentUser.ActivityResult = activities;
            
            ResponseMessageText = string.Concat(ResponseMessageText, $"\n {currentActivity.Name}");
        }
        else
        {
            const string activitiesNotFoundMessage = "По вашему запросу активностей не найдено";
            ResponseMessageText = string.Concat(ResponseMessageText, $"\n {activitiesNotFoundMessage}");
        }
        

        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetActivityPaginationKeyboard();
    }
}