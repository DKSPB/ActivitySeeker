using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.Controllers.Handlers;

public class ListOfActivitiesHandler: AbstractHandler
{
    public ListOfActivitiesHandler(ITelegramBotClient botClient, IUserService userService, CancellationToken cancellationToken, User currentUser) 
        : base(botClient, userService, cancellationToken, currentUser)
    { }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        var selectedActivityTypeId = callbackQuery.Data.Split('/')[1];
        CurrentUser.ActivityTypeId = Guid.Parse(selectedActivityTypeId);
        return Task.CompletedTask;
    }
}