using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using ActivitySeeker.Api.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : IHandler
{
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;
    private readonly IActivityTypeService _activityTypeService;
    private readonly ILogger<OfferHandler> _loggerHandler;
    private readonly ActivityPublisher _activityPublisher;

    public OfferHandler(IUserService userService, IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher, ILogger<OfferHandler> loggerHandler)
    {
        _userService = userService;
        _activityService = activityService;
        _activityTypeService = activityTypeService;
        _activityPublisher = activityPublisher;
        _loggerHandler = loggerHandler;
    }

    public async Task HandleAsync(UserDto currentUser, UserMessage userData)
    {
        currentUser.State.StateNumber = StatesEnum.AddOfferDescription;
        
        var activityTypes = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId is null).ToList();

        try
        {
            await _activityPublisher.EditMessageAsync(userData.ChatId, currentUser.State.MessageId, InlineKeyboardMarkup.Empty());
        }
        catch (Exception)
        {
            var errorMessage = "Пользователь очистил историю сообщений или открыл предложку, не нажимая кнопку старт";
            _loggerHandler.LogError(errorMessage);
        }

        var message = await _activityPublisher.SendMessageAsync(userData.ChatId, "Выбери тип активности", null, Keyboards.GetActivityTypesKeyboard(activityTypes));
        
        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}