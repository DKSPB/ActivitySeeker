using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.SaveOfferDate)]
public class SaveOfferDateHandler : AbstractHandler
{
    private readonly BotConfiguration _botConfig;
    private readonly string _webRootPath;
    private readonly ILogger<SaveOfferDateHandler> _logger;

    public SaveOfferDateHandler(
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher, 
        ILogger<SaveOfferDateHandler> logger,
        IOptions<BotConfiguration> botConfigOptions,
        IWebHostEnvironment webHostEnvironment) 
        : base(userService, activityService, activityPublisher)
    {
        _logger = logger;
        _botConfig = botConfigOptions.Value;
        _webRootPath = webHostEnvironment.WebRootPath; 
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

        if (parsingDateResult)
        {
            var result = DateTime.Compare(startActivityDate, DateTime.Now);

            if (result < 1)
            {
                Response.Text = $"Дата начала активности должна быть позднее текущей даты." +
                              $"\nВведите дату повторно:";
                Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
                Response.Keyboard = Keyboards.GetEmptyKeyboard();
            }
            else
            {
                CurrentUser.Offer.StartDate = startActivityDate;
                
                Response.Text = GetFinishOfferMessage(CurrentUser.Offer);
                //Response.Image = await GetImage(CurrentUser.
                Response.Keyboard = Keyboards.ConfirmOffer();
            }
        }
        else
        {
            Response.Text = $"Введёная дата не соответствует формату:" +
                          $"\n(дд.мм.гггг чч:мм)" +
                          $"\nПример: {DateTime.Now:dd.MM.yyyy HH:mm}";

            Response.Image = await GetImage(CurrentUser.State.StateNumber.ToString());
            Response.Keyboard = Keyboards.GetEmptyKeyboard();
        }
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
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