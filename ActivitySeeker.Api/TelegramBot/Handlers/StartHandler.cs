using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public StartHandler(ITelegramBotClient botClient, IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var chat = update.Message.Chat;
        
        currentUser.State.StateNumber = StatesEnum.MainMenu;

        var message = await _activityPublisher.SendMessageAsync(chat.Id, currentUser.State.ToString(), null, Keyboards.GetMainMenuKeyboard());

        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}