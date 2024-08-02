using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = ActivitySeeker.Domain.Entities.User;

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
        
        currentUser.State = StatesEnum.MainMenu;

        var message = await _botClient.SendTextMessageAsync(
            chat.Id,
            text: currentUser.ActivityRequest.ToString(),
            replyMarkup: Keyboards.GetMainMenuKeyboard(),
            cancellationToken: cancellationToken);

        currentUser.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}