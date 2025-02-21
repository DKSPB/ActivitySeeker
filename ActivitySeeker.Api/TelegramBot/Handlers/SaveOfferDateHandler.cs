using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.SaveOfferDate*/"/save_offer_date")]
public class SaveOfferDateHandler : AbstractHandler
{
    private readonly ILogger<SaveOfferDateHandler> _logger;

    public SaveOfferDateHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, 
        ILogger<SaveOfferDateHandler> logger) : base(userService, activityService, activityPublisher)
    {
        _logger = logger;
    }

    protected override Task ActionsAsync(UserUpdate userData)
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

        if (CurrentUser.Offer is null)
        {
            throw new ArgumentNullException($"Ошибка создания активности, объект offer is null");
        }

        var startActivityDateText = message;

        var parsingDateResult = DateParser.ParseDateTime(startActivityDateText, out var startActivityDate);

        if (parsingDateResult)
        {
            var result = DateTime.Compare(startActivityDate, DateTime.Now);

            if (result < 1)
            {
                Response.Text = $"Дата начала активности должна быть позднее текущей даты." +
                              $"\nВведите дату повторно:";
                
                Response.Keyboard = Keyboards.GetEmptyKeyboard();
            }
            else
            {
                CurrentUser.Offer.StartDate = startActivityDate;
                
                Response.Text = GetFinishOfferMessage(CurrentUser.Offer);
                Response.Keyboard = Keyboards.ConfirmOffer();
            }
        }
        else
        {
            Response.Text = $"Введёная дата не соответствует формату:" +
                          $"\n(дд.мм.гггг чч:мм)" +
                          $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            Response.Keyboard = Keyboards.GetEmptyKeyboard();
        }

        return Task.CompletedTask;
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