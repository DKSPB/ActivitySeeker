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

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserService _userService;
    private readonly ILogger<TelegramBotController> _logger;

    public TelegramBotController(IServiceProvider serviceProvider, IUserService userService, ILogger<TelegramBotController> logger)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken cancellationToken)
    {
        var handlerTypes = HandlerProvider.GetAllHandlerTypes(); 

        switch (update.Type)
        {
            case UpdateType.Message:
            {
                if (update.Message is null || update.Message.Text is null)
                {
                    _logger.LogError("Object Message is null or Message.Text is null");
                    throw new NullReferenceException("Object Message is null or Message.Text is null");
                }

                var currentUser = CreateUserIfNotExists(update.Message);

                var msgText = update.Message.Text;
                    
                if (msgText.Equals("/start"))
                {
                    var handler = _serviceProvider.GetRequiredService<SetDefaultSettingsHandler>();
                    await handler.HandleAsync(currentUser, update);
                    return Ok();
                }

                if (msgText.Equals("/offer"))
                {
                    var offerHandler = _serviceProvider.GetRequiredService<OfferHandler>();
                    await offerHandler.HandleAsync(currentUser, update);
                    return Ok();
                }

                var handlerType = HandlerProvider.FindHandlersTypeByState(handlerTypes, currentUser.State.StateNumber);

                if (handlerType is not null)
                {
                    var handler = HandlerProvider.CreateHandler(_serviceProvider, _logger, handlerType);
                    await handler.HandleAsync(currentUser, update);
                }

                return Ok();
            }
            case UpdateType.CallbackQuery:
            {
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery is null || callbackQuery.Data is null)
                {
                    _logger.LogError("Object Update.CallbackQuery is null or Update.CallbackQuery.Data is null or Update.CallbackQuery.Message is null");
                    throw new NullReferenceException("Object Update.CallbackQuery is null or Update.CallbackQuery.Data is null or Update.CallbackQuery.Message is null");
                }

                var currentUser = _userService.GetUserById(callbackQuery.From.Id)?? throw new NullReferenceException("User not found");

                var handlerType = handlerTypes.FirstOrDefault(x =>
                    x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState.GetDisplayName() == callbackQuery.Data);
                
                if (handlerType is null)
                {
                    handlerType = HandlerProvider.FindHandlersTypeByState(handlerTypes, currentUser.State.StateNumber);
                }

                if (handlerType is not null) 
                {
                    var handler = HandlerProvider.CreateHandler(_serviceProvider, _logger, handlerType);
                    await handler.HandleAsync(currentUser, update);
                }
                
                return Ok();
            }
        }
        return Ok();
    }

    #region Private methods

    /// <summary>
    /// Создание пользователя, если о нём нет записи в БД
    /// </summary>
    /// <param name="message">Сообщение от пользователя</param>
    private UserDto CreateUserIfNotExists(Message message)
    {
        var telegramUser = message.From;
        if (telegramUser is null)
        {
            throw new NullReferenceException("Object user is null");
        }

        var user = _userService.GetUserById(telegramUser.Id);

        if (user is not null)
        {
            return user;
        }
        
        user = new UserDto
        {
            Id = telegramUser.Id,
            UserName = telegramUser.Username ?? "",
            ChatId = message.Chat.Id,
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

    #endregion
}