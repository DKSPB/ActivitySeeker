using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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
    private readonly ILogger<OfferHandler> _loggerHandler;
    private readonly ActivityPublisher _activityPublisher;

    public OfferHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, ILogger<OfferHandler> loggerHandler)
    {
        _userService = userService;
        _activityService = activityService;
        _activityPublisher = activityPublisher;
        _loggerHandler = loggerHandler;
    }

    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        Chat? chat = null;
        if (update.Message is not null)
        {
            chat = update.Message.Chat;
        }

        if (update.CallbackQuery is not null)
        {
            chat = update.CallbackQuery.Message.Chat;
        }

        currentUser.State.StateNumber = StatesEnum.AddOfferDescription;
        var activityTypes = _activityService.GetActivityTypes();

        try
        {
            await _activityPublisher.EditMessageAsync(chat.Id, currentUser.State.MessageId, InlineKeyboardMarkup.Empty());
        }
        catch (Exception)
        {
            var errorMessage = "Пользователь очистил историю сообщений или открыл предложку, не нажимая кнопку старт";
            _loggerHandler.LogError(errorMessage);
        }

        var message = await _activityPublisher.SendMessageAsync(chat.Id, "Выбери тип активности", null, Keyboards.GetActivityTypesKeyboard(activityTypes));
        
        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }

    public async Task HandleAsync(ChatId chatId, UserDto currentUser, string? msgText = null)
    {
        currentUser.State.StateNumber = StatesEnum.AddOfferDescription;
        var activityTypes = _activityService.GetActivityTypes();

        try
        {
            await _activityPublisher.EditMessageAsync(chatId, currentUser.State.MessageId, InlineKeyboardMarkup.Empty());
        }
        catch (Exception)
        {
            var errorMessage = "Пользователь очистил историю сообщений или открыл предложку, не нажимая кнопку старт";
            _loggerHandler.LogError(errorMessage);
        }

        var message = await _activityPublisher.SendMessageAsync(chatId, "Выбери тип активности", null, Keyboards.GetActivityTypesKeyboard(activityTypes));

        currentUser.State.MessageId = message.MessageId;
        _userService.UpdateUser(currentUser);
    }
}