using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class SelectActivityPeriodHandler: AbstractHandler
{
    private const string MessageText = "Выбери период проведения активности:";
    
    public SelectActivityPeriodHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetPeriodActivityKeyboard();
    }
}