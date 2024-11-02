using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public interface IHandler
{
    Task HandleAsync(UserDto currentUser, UserUpdate userUpdate);
}