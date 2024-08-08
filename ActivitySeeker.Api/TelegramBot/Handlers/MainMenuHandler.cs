using ActivitySeeker.Api.Controllers;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.MainMenu)]
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
        ResponseMessageText = CurrentUser.State.ToString();
        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return Keyboards.GetMainMenuKeyboard();
    }
}