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
using User = Telegram.Bot.Types.User;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IUserService _userService;

    public TelegramBotController(IServiceProvider serviceProvider, IUserService userService)
    {
        _serviceProvider = serviceProvider;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken cancellationToken)
    {
        var handlerTypes = GetAllHandlerTypes(); 

        switch (update.Type)
        {
            case UpdateType.Message:
            {
                if (update.Message != null)
                {
                    var currentUser = CreateUserIfNotExists(update.Message);
                    
                    if (update.Message.Text is not null && update.Message.Text.Equals("/start"))
                    {
                        var handler = _serviceProvider.GetRequiredService<StartHandler>();
                        await handler.HandleAsync(currentUser, update, cancellationToken);
                        return Ok();
                    }

                    if (update.Message.Text is not null && update.Message.Text.Equals("/offer"))
                    {
                        var offerHandler = _serviceProvider.GetRequiredService<OfferHandler>();
                        await offerHandler.HandleAsync(currentUser, update, cancellationToken);
                        return Ok();
                    }

                    var handlerType = FindHandlerByState(handlerTypes, currentUser.State.StateNumber);

                    if (handlerType is not null)
                    {
                        await HandleCommand(handlerType, currentUser,update, cancellationToken);
                    }
                }
                else
                {
                    throw new NullReferenceException("Object Message is null");
                }
                return Ok();
            }
            case UpdateType.CallbackQuery:
            {
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery is null)
                {
                    throw new NullReferenceException("Callback query is null");
                }
                
                if (callbackQuery.Data is null)
                {
                    throw new NullReferenceException("Object Data is null");
                }

                var currentUser = _userService.GetUserById(callbackQuery.From.Id); 

                var callbackData = callbackQuery.Data;
                
                var handlerType = handlerTypes.FirstOrDefault(x =>
                    x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState.GetDisplayName() == callbackData);
                
                if (handlerType is null)
                {
                    handlerType = FindHandlerByState(handlerTypes, currentUser.State.StateNumber);
                }

                await HandleCommand(handlerType, currentUser, update, cancellationToken);
                
                return Ok();
            }
        }
        return Ok();
    }

    #region Private methods

    /// <summary>
    /// Получить все обработчики команд кроме интерфейса и абстрактного класса
    /// </summary>
    /// <returns></returns>
    private IEnumerable<Type> GetAllHandlerTypes()
    {
        var type = typeof(IHandler);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p)).Where(type => type is { IsClass: true, IsAbstract: false });
    }
    
    /// <summary>
    /// Создать экземпляр команды и выполнить на нём метод Handle
    /// </summary>
    /// <param name="handlerType"></param>
    /// <param name="currentUser"></param>
    /// <param name="update"></param>
    /// <param name="cancellationToken"></param>
    private async Task HandleCommand(Type handlerType, UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService(handlerType) as IHandler;
        await handler.HandleAsync(currentUser,update, cancellationToken);
    }

    /// <summary>
    /// Найти обработчик команды по состоянию диалога
    /// </summary>
    /// <param name="handlerTypes"></param>
    /// <param name="state"></param>
    /// <returns></returns>
    private Type? FindHandlerByState(IEnumerable<Type> handlerTypes, StatesEnum state)
    {
        return handlerTypes.FirstOrDefault(x =>
            x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState == state);
    }

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