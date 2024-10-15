using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    
    public StartHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    
    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var chat = update.Message.Chat;
        
        currentUser.State.StateNumber = StatesEnum.MainMenu;

        var message = await _botClient.SendTextMessageAsync(
            chat.Id,
            text: currentUser.State.ToString(),
            replyMarkup: Keyboards.GetMainMenuKeyboard(),
            cancellationToken: cancellationToken);

        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}