using System.Reflection;
using ActivitySeeker.Api.TelegramBot;
using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
using Telegram.Bot.Types.Enums;

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
                    var currentUser = _userService.GetUserById(update.Message.From.Id);
                    
                    if (update.Message.Text is not null && update.Message.Text.Equals("/start"))
                    {
                        var handler = _serviceProvider.GetRequiredService<StartHandler>();
                        await handler.HandleAsync(currentUser, update, cancellationToken);
                        return Ok();
                    }

                    var handlerType = FindHandlerByState(handlerTypes, currentUser.State);

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
                    handlerType = FindHandlerByState(handlerTypes, currentUser.State);
                }

                await HandleCommand(handlerType, currentUser, update, cancellationToken);
                
                return Ok();
            }
        }
        return Ok();
    }

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

    private async Task HandleCommand(Type handlerType, UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var handler = _serviceProvider.GetRequiredService(handlerType) as IHandler;
        await handler.HandleAsync(currentUser,update, cancellationToken);
    }

    private Type? FindHandlerByState(IEnumerable<Type> handlerTypes, StatesEnum state)
    {
        return handlerTypes.FirstOrDefault(x =>
            x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState == state);
    }
}