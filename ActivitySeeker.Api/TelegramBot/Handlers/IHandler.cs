using ActivitySeeker.Bll.Models;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public interface IHandler
{
    Task HandleAsync(UserDto currentUser, Update update);
}