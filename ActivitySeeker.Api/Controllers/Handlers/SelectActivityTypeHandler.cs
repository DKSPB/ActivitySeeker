using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.Controllers.Handlers;

public class SelectActivityTypeHandler: AbstractHandler
{
    public SelectActivityTypeHandler(ITelegramBotClient botClient, IUserService userService, 
        CancellationToken cancellationToken, User currentUser): 
        base(botClient, userService, cancellationToken, currentUser)
    { }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        return Task.CompletedTask;
    }
}