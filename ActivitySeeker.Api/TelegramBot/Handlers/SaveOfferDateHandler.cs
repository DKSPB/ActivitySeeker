using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDate)]
public class SaveOfferDateHandler : IHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    private readonly ILogger<SaveOfferDateHandler> _logger;

    public SaveOfferDateHandler(IUserService userService, ActivityPublisher activityPublisher, 
        ILogger<SaveOfferDateHandler> logger)
    {
        _logger = logger;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }

    public async Task HandleAsync(UserDto currentUser, UserMessage userData)
    {
        var message = userData.Data;

        if (message is null)
        {
            var errorMessage = "Объект update.Message is null";
            _logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }

        if (message is null)
        {
            var errorMessage = "Объект update.Message.Text is null";
            _logger.LogError(errorMessage);
            throw new ArgumentNullException(errorMessage);
        }
        
        if (currentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности, объект offer is null");
        }
        
        var startActivityDateText = message;

        var parsingDateResult = DateParser.ParseDateTime(startActivityDateText, out var startActivityDate);

        if (parsingDateResult)
        {
            currentUser.Offer.StartDate = startActivityDate;

            var feedbackMessage = await _activityPublisher.SendMessageAsync(userData.ChatId, 
                GetFinishOfferMessage(currentUser.Offer), null, Keyboards.ConfirmOffer());
                
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
        else
        {
            var msgText = $"Введёная дата не соответствует формату:" +
                $"\n(дд.мм.гггг чч:мм)" +
                $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}";

            var feedbackMessage = await _activityPublisher.SendMessageAsync(userData.ChatId, msgText);
                
            currentUser.State.MessageId = feedbackMessage.MessageId;
            _userService.UpdateUser(currentUser);
        }
    }

    private string GetFinishOfferMessage(ActivityDto offer)
    {
        List<string> prefix = new()
        {
            "Эта активность будет предложена для публикации.",
            "Убедись, что данные заполнены корректно:"
            
        };
        return offer.GetActivityDescription(prefix).ToString();
    }
}