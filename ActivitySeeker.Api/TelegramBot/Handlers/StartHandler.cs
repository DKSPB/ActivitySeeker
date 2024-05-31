using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class StartHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    
    public StartHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    
    public async Task HandleAsync(Update update, CancellationToken cancellationToken)
    {
        if (update.Message != null)
        {
            var chat = update.Message.Chat;
            try
            {
                if (update.Message.Text is not null && update.Message.Text.Equals("/start"))
                {
                    var user = update.Message.From;
                    if (user is null)
                    {
                        throw new NullReferenceException("User in null");
                    }

                    var message = await _botClient.SendTextMessageAsync(
                        chat.Id,
                        text: "Выбери тип активности и время проведения",
                        replyMarkup: Keyboards.GetMainMenuKeyboard(),
                        cancellationToken: cancellationToken);
                    
                    var currentUser = new UserDto
                    {
                        Id = user.Id,
                        UserName = user.Username ?? "",
                        ChatId = chat.Id,
                        MessageId = message.MessageId,
                    };
                    
                    _userService.CreateOrUpdateUser(currentUser);
                }
                else
                {
                    await _botClient.SendTextMessageAsync(
                        chat.Id, "Нераспознанная команда", cancellationToken: cancellationToken);
                }
            }
            catch (Exception e)
            {
                await _botClient.SendTextMessageAsync(
                    chat.Id, e.Message, cancellationToken: cancellationToken);
            }
        }
        else
        {
            throw new NullReferenceException("Object Message is null");
        }
    }
}