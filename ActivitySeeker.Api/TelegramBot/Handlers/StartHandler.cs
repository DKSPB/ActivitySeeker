using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Start)]
public class StartHandler: IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    
    public StartHandler(IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    
    public async Task HandleAsync(UserDto currentUser, UserUpdate userData)
    {
        var message = await _activityPublisher.SendMessageAsync(
            userData.ChatId, 
            currentUser.State.ToString(),
            null, 
            Keyboards.GetMainMenuKeyboard());
         
        currentUser.State.StateNumber = StatesEnum.MainMenu;
        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}