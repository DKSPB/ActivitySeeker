using Telegram.Bot.Types;
using Microsoft.AspNetCore.Mvc;
using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.Enums;
using System.Windows.Input;
using System.Reflection;

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
                        IHandler handler = _serviceProvider.GetRequiredService<StartHandler>();
                        await handler.HandleAsync(currentUser, update, cancellationToken);
                    }

                    if (currentUser.State == StatesEnum.PeriodFromDate)
                    {
                        IHandler handler = _serviceProvider.GetRequiredService<UserSetFromDateHandler>();
                        await handler.HandleAsync(currentUser, update, cancellationToken);
                        return Ok();
                    }

                    if (currentUser.State == StatesEnum.PeriodToDate)
                    {
                        IHandler handler = _serviceProvider.GetRequiredService<UserSetByDateHandler>();
                        await handler.HandleAsync(currentUser,update, cancellationToken);
                        return Ok();
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
                
                foreach (Type handlerType in handlerTypes) 
                {
                    if ( handlerType.Name == callbackData )
                    {
                        IHandler? handler = _serviceProvider.GetRequiredService(handlerType) as IHandler;

                        if (handler == null) 
                                throw new ArgumentException("Unrecognized handler, Не получилось созать экземпляр обработчика");

                        await handler.HandleAsync(currentUser,update, cancellationToken);
                        return Ok();
                    }
                }
                
                if (currentUser.State == StatesEnum.ActivityTypeChapter)
                {
                    IHandler handler = _serviceProvider.GetRequiredService<ListOfActivitiesHandler>();
                    await handler.HandleAsync(currentUser,update, cancellationToken);
                }

                return Ok();
            }
        }

        return Ok();
    }

    private IEnumerable<Type> GetAllHandlerTypes()
    {
        var type = typeof(IHandler);
        return AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(s => s.GetTypes())
            .Where(p => type.IsAssignableFrom(p)).Where(type => type.IsClass);
    }
}