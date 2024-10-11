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

        var result = offerLink != null && isLink(offerLink);
        
        if (result)
        {
            currentUser.State.StateNumber = StatesEnum.SaveOfferDate;
            currentUser.Offer.Link = offerLink;

            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: $"Введите дату мероприятия в формате (дд.мм.гггг чч.мм):" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);

            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            currentUser.State.StateNumber = StatesEnum.OfferActivityLink;

            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: "Поле ссылки содержит неправильные символы, либо же оно пустое или содержит пробелы!",
                cancellationToken: cancellationToken);

            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }

    private bool isLink(string offerLink)
    {
        if (!string.IsNullOrWhiteSpace(offerLink))
        {
            if (offerLink.Contains("t.me") || offerLink.Contains("vk.com"))
            {
                return true;
            }
        }

        return false;
    }
}