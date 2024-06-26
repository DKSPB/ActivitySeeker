using ActivitySeeker.Api.Controllers;
using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class MainMenuHandler: AbstractHandler
{
    private const string MessageText = "Выбери тип активности и время проведения:";

    public MainMenuHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService):
        base(botClient, userService, activityService)
    {
        ResponseMessageText = MessageText;
    }
    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        ResponseMessageText = CurrentUser.ActivityRequest.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}