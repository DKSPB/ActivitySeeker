using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : IHandler
{
    private readonly IUserService _userService;
    private readonly IActivityTypeService _activityTypeService;
    private readonly ActivityPublisher _activityPublisher;

    public OfferHandler(IUserService userService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityTypeService = activityTypeService;
        _activityPublisher = activityPublisher;
    }

    public async Task HandleAsync(UserDto currentUser, UserUpdate userData)
    {
        currentUser.State.StateNumber = StatesEnum.AddOfferDescription;

        var activityTypes = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId is null).ToList();
            
        await _activityPublisher.EditMessageAsync(userData.ChatId, currentUser.State.MessageId, InlineKeyboardMarkup.Empty());


        var message = await _activityPublisher.SendMessageAsync(
            userData.ChatId, "Выбери тип активности", 
            null, 
            Keyboards.GetActivityTypesKeyboard(activityTypes, "К поиску активностей"));
        
        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}