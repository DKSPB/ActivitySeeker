using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Api.TelegramBot.Handlers;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly StartHandler _startHandler;
    private readonly HandlerFactory _handlerFactory;

    public TelegramBotController(/*MessageHandler messageHandler*/ HandlerFactory handlerFactory, StartHandler startHandler)
    {
        _startHandler = startHandler;
        _handlerFactory = handlerFactory;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
            {
                await _startHandler.HandleAsync(update, cancellationToken);
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
                
                var handler = _handlerFactory.GetHandler(callbackQuery.Data);
                await handler.HandleAsync(callbackQuery, cancellationToken);
                
                return Ok();
            }
        }
        return Ok();
    }
}