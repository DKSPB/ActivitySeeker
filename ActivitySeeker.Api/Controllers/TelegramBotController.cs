using ActivitySeeker.Api.TelegramBot;
using ActivitySeeker.Api.TelegramBot.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly HandlerFactory _handlerFactory;

    public TelegramBotController(HandlerFactory handlerFactory)
    {
        _handlerFactory = handlerFactory;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken cancellationToken)
    {
        var handler = _handlerFactory.GetHandler(update);
        await handler.HandleAsync(update, cancellationToken);
                
        return Ok();
    }
}