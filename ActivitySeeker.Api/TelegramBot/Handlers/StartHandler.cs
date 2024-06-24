using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class StartHandler: IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    
    public StartHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    
    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        var chat = update.Message.Chat;

        var user = update.Message.From;
        if (user is null)
        {
            throw new NullReferenceException("User in null");
        }

        var currentUser = new UserDto
        {
            Id = user.Id,
            UserName = user.Username ?? "",
            ChatId = chat.Id,
            State = StatesEnum.MainMenu
        };
        
        var message = await _botClient.SendTextMessageAsync(
            chat.Id,
            text: currentUser.ActivityRequest.ToString(),
            replyMarkup: Keyboards.GetMainMenuKeyboard(),
            cancellationToken: cancellationToken);

        currentUser.MessageId = message.MessageId;
        _userService.CreateOrUpdateUser(currentUser);
    }
}