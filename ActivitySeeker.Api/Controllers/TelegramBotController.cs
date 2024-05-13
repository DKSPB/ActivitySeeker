using ActivitySeeker.Api.TelegramBot;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.Controllers;

[ApiController]
[Route("api/message")]
public class TelegramBotController: ControllerBase
{
    private readonly MessageHandler _messageHandler;

    public TelegramBotController(MessageHandler messageHandler)
    {
        _messageHandler = messageHandler;
    }

    [HttpPost]
    public async Task<IActionResult> UpdateReceived([FromBody]Update update, CancellationToken token)
    {
        await _messageHandler.Handle(update, token);
        return Ok();

    }
}