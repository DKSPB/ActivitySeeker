using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDescription)]
public class SaveOfferDescriptionHandler : IHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;

    public SaveOfferDescriptionHandler(ITelegramBotClient botClient, IUserService userService)
    {
        _botClient = botClient;
        _userService = userService;
    }
    public async Task HandleAsync(UserDto currentUser, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message;
        
        var offerDescription = update.Message.Text;
        
        if (currentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности,  offer is null");
        }
        
        if (!string.IsNullOrWhiteSpace(offerDescription))
        {
            currentUser.State.StateNumber = StatesEnum.SaveOfferDate;
            currentUser.Offer.Description = offerDescription;

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
            currentUser.State.StateNumber = StatesEnum.AddOfferDescription;
            
            var feedbackMessage = await _botClient.SendTextMessageAsync(
                message.Chat.Id,
                text: "Поле описания не может быть пустым!",
                cancellationToken: cancellationToken);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
}