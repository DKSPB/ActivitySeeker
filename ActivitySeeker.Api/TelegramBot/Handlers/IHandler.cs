using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public interface IHandler
{
    Task HandleAsync(Update update, CancellationToken cancellationToken);
}