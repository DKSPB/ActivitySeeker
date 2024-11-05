using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferFormat)]
public class SaveOfferFormat: AbstractHandler
{
    private readonly ILogger<SaveOfferFormat> _logger;
    
    public SaveOfferFormat(ILogger<SaveOfferFormat> logger, IUserService userService, 
        IActivityService activityService, ActivityPublisher activityPublisher) 
        : base(userService, activityService, activityPublisher)
    {
        _logger = logger;
    }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        if (CurrentUser.Offer is null)
        {
            var msg = $"Объект CurrentUser.Offer = null";
            _logger.LogError(msg);
            throw new ArgumentNullException(msg);
        }
        
        CurrentUser.Offer.IsOnline = userData.Data.Equals("online");
        CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        
        ResponseMessageText = $"Заполни описание события";


        return Task.CompletedTask;
    }
}