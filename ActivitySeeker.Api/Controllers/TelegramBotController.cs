using System.Reflection;
using ActivitySeeker.Api.TelegramBot;
using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Extensions;
using Telegram.Bot.Types.Enums;
using ActivitySeeker.Api.Models;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly IUserService _userService;
    private readonly IServiceProvider _serviceProvider;
    private readonly ActivityPublisher _activityPublisher;
    private readonly ILogger<TelegramBotController> _logger;

    public TelegramBotController(IServiceProvider serviceProvider, IUserService userService, ActivityPublisher activityPublisher, ILogger<TelegramBotController> logger)
    {
        _activityPublisher = activityPublisher;
        _serviceProvider = serviceProvider;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken cancellationToken)
    {
        var handlerTypes = HandlerProvider.GetAllHandlerTypes();

        var userMessage = GetUserMessageData(update);

        if(userMessage is null)
        {
            return Ok();
        }

        if (userMessage.CallbackQueryId is not null)
        {
            await _activityPublisher.AnswerOnPushButton(userMessage.CallbackQueryId);
        }

        var currentUser = CreateUserIfNotExists(userMessage);

        IHandler handler;

        if (userMessage.Data.Equals("/start"))
        {
            if (currentUser.CityId is null)
            {
                handler = _serviceProvider.GetRequiredService<SetDefaultSettingsHandler>();
            }
            else
            {
                handler = _serviceProvider.GetRequiredService<StartHandler>();
            }
            await handler.HandleAsync(currentUser, update);
            return Ok();
        }

        if (userMessage.Data.Equals("/offer"))
        {
            var offerHandler = _serviceProvider.GetRequiredService<OfferHandler>();
            await offerHandler.HandleAsync(currentUser, update);
            return Ok();
        }

        var handlerType = HandlerProvider.FindhandlersTypeByCallbackData(handlerTypes, userMessage.Data);

        if (handlerType is null)
        {
            handlerType = HandlerProvider.FindHandlersTypeByState(handlerTypes, currentUser.State.StateNumber);
        }

        if(handlerType is null)
        {
            return Ok();
        }
              
        handler = HandlerProvider.CreateHandler(_serviceProvider, _logger, handlerType);
        await handler.HandleAsync(currentUser, update);
        return Ok();
    }

    #region Private methods

    /// <summary>
    /// Создание пользователя, если о нём нет записи в БД
    /// </summary>
    /// <param name="message">Сообщение от пользователя</param>
    private UserDto CreateUserIfNotExists(UserMessage message)
    {
        var user = _userService.GetUserById(message.TelegramUserId);

        if (user is not null)
        {
            return user;
        }
        
        user = new UserDto
        {
            Id = message.TelegramUserId,
            UserName = message.TelegramUsername ?? "",
            ChatId = message.ChatId,
            State =
            {
                SearchFrom = DateTime.Now,
                SearchTo = DateTime.Now.AddDays(1).Date,
                MessageId = message.MessageId
            }
        };
        _userService.CreateUser(user);

        return user;
    }

    private UserMessage? GetUserMessageData(Update update)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            if (message?.Text is not null && message?.From is not null)
            {
                return new UserMessage
                {
                    TelegramUserId = message.From.Id,
                    TelegramUsername = message.From.Username,
                    ChatId = message.Chat.Id,
                    Data = message.Text
                };
            }

            return null;
            
        }

        if (update.Type == UpdateType.CallbackQuery)
        {
            var callbackQuery = update.CallbackQuery;

            if (callbackQuery?.Data is not null && callbackQuery?.Message is not null && callbackQuery.From is not null)
            {
                return new UserMessage
                {
                    TelegramUserId = callbackQuery.From.Id,
                    TelegramUsername= callbackQuery.From.Username,
                    MessageId = callbackQuery.Message.MessageId,
                    CallbackQueryId = callbackQuery.Id,
                    ChatId = callbackQuery.Message.Chat.Id,
                    Data = callbackQuery.Data
                };
            }

            return null;
        }

        return null;
    }

    #endregion
}