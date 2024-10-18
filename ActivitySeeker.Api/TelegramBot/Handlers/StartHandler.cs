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
    private readonly ActivityPublisher _activityPublisher;
    
    public StartHandler(ITelegramBotClient botClient, IUserService userService, ActivityPublisher activityPublisher)
    {
        _botClient = botClient;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var chat = update.Message.Chat;
        
        currentUser.State.StateNumber = StatesEnum.MainMenu;

        var message = await _activityPublisher.PublishActivity(chat.Id, currentUser.State.ToString(), null, Keyboards.GetToMainMenuKeyboard());

        /*var message = await _botClient.SendTextMessageAsync(
            chat.Id,
            text: currentUser.State.ToString(),
            replyMarkup: Keyboards.GetMainMenuKeyboard(),
            cancellationToken: cancellationToken);*/


        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}