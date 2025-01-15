using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class OfferMenuHandler: AbstractHandler
{
    public OfferMenuHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    {
    }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        throw new NotImplementedException();
    }
}