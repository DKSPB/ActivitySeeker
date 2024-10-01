using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveActivityLink)]
public class SaveActivityLinkHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;

    public SaveActivityLinkHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }

    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        
        var offerLink = update.Message.Text;
        
        if (currentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности,  offer is null");
        }
        
        if (!string.IsNullOrWhiteSpace(offerLink))
        {
            currentUser.State.StateNumber = StatesEnum.AddOfferDate;
            currentUser.Offer.Link = offerLink;

            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Ссылка добавлена",
                cancellationToken: cancellationToken);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            currentUser.State.StateNumber = StatesEnum.OfferActivityLink;
            
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: "Поле ссылки не может быть пустым или содержать только пробелы!",
                cancellationToken: cancellationToken);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
}