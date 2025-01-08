using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDate)]
public class SaveOfferDateHandler : AbstractHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;
    private readonly ILogger<SaveOfferDateHandler> _logger;

    private InlineKeyboardMarkup _keyboard = Keyboards.GetEmptyKeyboard();

    public SaveOfferDateHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, 
        ILogger<SaveOfferDateHandler> logger) : base(userService, activityService, activityPublisher)
    {
        _logger = logger;
        _userService = userService;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
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

        var result = DateTime.Compare(startActivityDate, DateTime.Now);

        if (result < 1)
        {
            ResponseMessageText = $"Дата начала активности должна быть позднее текущей даты." +
                          $"\nВведите дату повторно:";
        }
        else
        {
            if (parsingDateResult)
            {
                CurrentUser.Offer.StartDate = startActivityDate;

                _keyboard = Keyboards.ConfirmOffer();
            }
            else
            {
                ResponseMessageText = $"Введёная дата не соответствует формату:" +
                              $"\n(дд.мм.гггг чч:мм)" +
                              $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}";
            }
        }
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return _keyboard;
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