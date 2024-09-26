using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;

    public OfferHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
    {
        _botClient = botClient;
        _userService = userService;
        _activityService = activityService;
    }

    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var chat = update.Message.Chat;
        
        currentUser.State.StateNumber = StatesEnum.SelectListOfferType;
        var activityTypes = _activityService.GetActivityTypes();
        
        var message = await _botClient.SendTextMessageAsync(
            chat.Id,
            text: "Выберите тип активности",
            replyMarkup: Keyboards.GetActivityTypesKeyboard(activityTypes),
            cancellationToken: cancellationToken
            );
        
        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}