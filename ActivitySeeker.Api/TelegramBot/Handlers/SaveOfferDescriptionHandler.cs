using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDescription)]
public class SaveOfferDescriptionHandler : IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;

    public SaveOfferDescriptionHandler(ITelegramBotClient botClient, IUserService userService, ActivityPublisher activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var message = update.Message;
        
        var offerDescription = update.Message.Text;
        
        if (currentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности,  offer is null");
        }
        
        if (!string.IsNullOrWhiteSpace(offerDescription) && offerDescription.Length <= 2000)
        {
            var msgText = $"Заполни дату и время проведения события в формате: (дд.мм.гггг чч.мм):" +
                      $"\nПример:{DateTime.Now:dd.MM.yyyy HH:mm}";

            currentUser.State.StateNumber = StatesEnum.SaveOfferDate;
            currentUser.Offer.LinkOrDescription = offerDescription;

            var feedbackMessage = await _activityPublisher.SendMessageAsync(message.Chat.Id, msgText);
            
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var msgText = "Описание события не может быть пустым, состоять только из пробелов и содержать больше 2000 символов";

            currentUser.State.StateNumber = StatesEnum.AddOfferDescription;

            var feedbackMessage = await _activityPublisher.SendMessageAsync(message.Chat.Id, msgText);
                
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }
}