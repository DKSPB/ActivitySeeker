using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class NextHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    public NextHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : 
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        if (CurrentUser.ActivityResult.Count > 0)
        {
            var currentActivity = CurrentUser.ActivityResult.First(x => x.Selected);
            
            var nextNode = CurrentUser.ActivityResult.Find(currentActivity)?.Next;
            if (nextNode is not null)
            {
                currentActivity.Selected = false;
                nextNode.Value.Selected = true;
            }
            else
            {
                const string activitiesNotFoundMessage = "Больше активностей не найдено";
                ResponseMessageText = string.Concat(ResponseMessageText, $"\n {activitiesNotFoundMessage}");
            }
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
        throw new NotImplementedException();
    }
}