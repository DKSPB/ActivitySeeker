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
                        await handler.HandleAsync(update, cancellationToken);
                    }

                    if (currentUser.State == StatesEnum.PeriodFromDate)
                    {
                        IHandler handler = _serviceProvider.GetRequiredService<UserSetFromDateHandler>();
                        await handler.HandleAsync(update, cancellationToken);
                    }

                    if (currentUser.State == StatesEnum.PeriodToDate)
                    {
                        IHandler handler = _serviceProvider.GetRequiredService<UserSetByDateHandler>();
                        await handler.HandleAsync(update, cancellationToken);
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

                var callbackData = callbackQuery.Data;
                
                foreach (Type handlerType in handlerTypes) 
                {
                    if( handlerType.Name == callbackData )
                    {
                        IHandler? handler = _serviceProvider.GetRequiredService(handlerType) as IHandler;

                        if (handler == null) 
                                throw new ArgumentException("Unrecognized handler, Не получилось созать экземпляр обработчика");

                        await handler.HandleAsync(update, cancellationToken);
                        break;
                    }
                }
                /*if (callbackData.Equals("mainMenu"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<MainMenuHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/
                
                /*if (callbackData.Equals("selectActivityTypeButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectActivityTypeHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/

                /*if (callbackData.Equals("searchActivityButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SearchResultHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/

                /*if (callbackData.Equals("back"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<PreviousHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/

                /*if (callbackData.Equals("next"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<NextHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/

                /*if (callbackData.Equals("activityStartPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectActivityPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/

                /*if (callbackData.Equals("todayPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectTodayPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                if (callbackData.Equals("tomorrowPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectTomorrowPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                if (callbackData.Equals("afterTomorrowPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectAfterTomorrowPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                if (callbackData.Equals("weekPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectWeekPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                if (callbackData.Equals("monthPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectMonthPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                if (callbackData.Equals("userPeriodButton"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<SelectUserPeriodHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }*/
                
                if (callbackData.Contains("activityType"))
                {
                    IHandler handler = _serviceProvider.GetRequiredService<ListOfActivitiesHandler>();
                    await handler.HandleAsync(update, cancellationToken);
                }

                return Ok();//throw new ArgumentException("Unrecognized handler");
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