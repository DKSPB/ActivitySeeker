using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class PreviousHandler: AbstractHandler
{
    private const string MessageText = "Найденные активности:";
    
    public PreviousHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : 
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        if (CurrentUser.ActivityResult.Count > 0)
        {
            var currentActivity = CurrentUser.ActivityResult.First(x => x.Selected);
            
            var previousNode = CurrentUser.ActivityResult.Find(currentActivity)?.Previous;
            if (previousNode is not null)
            {
                currentActivity.Selected = false;
                previousNode.Value.Selected = true;
                ResponseMessageText = previousNode.Value.Name;
            }
            else
            {
                ResponseMessageText = currentActivity.Name;
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
        return Keyboards.GetActivityPaginationKeyboard();
    }

    protected override async Task EditPreviousMessage(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await BotClient.DeleteMessageAsync(
            callbackQuery.Message.Chat.Id,
            CurrentUser.MessageId,
            cancellationToken);
    }
}