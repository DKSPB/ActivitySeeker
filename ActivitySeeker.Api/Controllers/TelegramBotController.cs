using ActivitySeeker.Api.TelegramBot;
using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Microsoft.AspNetCore.Authorization;
using Telegram.Bot.Types.Enums;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Domain.Entities;

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

    public TelegramBotController(IServiceProvider serviceProvider, IUserService userService, 
        ActivityPublisher activityPublisher, ILogger<TelegramBotController> logger)
    {
        _activityPublisher = activityPublisher;
        _serviceProvider = serviceProvider;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update)
    {
        var handlerTypes = HandlerProvider.GetAllHandlerTypes().ToList();

        var userUpdate = GetUserMessageData(update);

        if(userUpdate is null)
        {
            return Ok();
        }

        if (userUpdate.CallbackQueryId is not null)
        {
            await _activityPublisher.AnswerOnPushButton(userUpdate.CallbackQueryId);
        }

        var currentUser = CreateUserIfNotExists(userUpdate);

        IHandler handler;

        if (userUpdate.Data.Equals("/start"))
        {
            handler = _serviceProvider.GetRequiredService<StartHandler>();
            await handler.HandleAsync(currentUser, userUpdate);
            return Ok();
        }

        if (userUpdate.Data.Equals("/offer"))
        {
            handler = _serviceProvider.GetRequiredService<OfferHandler>();
            await handler.HandleAsync(currentUser, userUpdate);
            return Ok();
        }

        if(userUpdate.Data.Equals("/city"))
        {
            handler = _serviceProvider.GetRequiredService<SetDefaultSettingsHandler>();
            await handler.HandleAsync(currentUser, userUpdate);
            return Ok();
        }

        var handlerType = HandlerProvider.FindHandlersTypeByCallbackData(handlerTypes, userUpdate.Data) ??
                          HandlerProvider.FindHandlersTypeByState(handlerTypes, currentUser.State.StateNumber);

        if(handlerType is null)
        {
            return Ok();
        }
              
        handler = HandlerProvider.CreateHandler(_serviceProvider, _logger, handlerType);
        await handler.HandleAsync(currentUser, userUpdate);
        return Ok();
    }

    #region Private methods

    /// <summary>
    /// Создание пользователя, если о нём нет записи в БД
    /// </summary>
    /// <param name="update">Сообщение от пользователя</param>
    private UserDto CreateUserIfNotExists(UserUpdate update)
    {
        var user = _userService.GetUserById(update.TelegramUserId);

        if (user is not null)
        {
            return user;
        }
        
        user = new UserDto
        {
            Id = update.TelegramUserId,
            UserName = update.TelegramUsername ?? "",
            ChatId = update.ChatId,
            State =
            {
                StateNumber = StatesEnum.Start,
                SearchFrom = DateTime.Now,
                SearchTo = DateTime.Now.AddDays(1).Date,
                MessageId = update.MessageId
            }
        };
        _userService.CreateUser(user);

        return user;
    }

    private static UserUpdate? GetUserMessageData(Update update)
    {
        if (update.Type == UpdateType.Message)
        {
            var message = update.Message;

            if (message?.Text is not null && message.From is not null)
            {
                return new UserUpdate
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

            if (callbackQuery?.Data is not null && callbackQuery.Message is not null)
            {
                return new UserUpdate
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