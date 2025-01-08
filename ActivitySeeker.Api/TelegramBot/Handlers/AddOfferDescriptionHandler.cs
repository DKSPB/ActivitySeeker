using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    public AddOfferDescriptionHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService, ActivityPublisher activityPublisher)
        : base(userService, activityService, activityPublisher)
    {}

    protected override Task ActionsAsync(UserUpdate userData)
    {
        return Task.CompletedTask;
    }
}