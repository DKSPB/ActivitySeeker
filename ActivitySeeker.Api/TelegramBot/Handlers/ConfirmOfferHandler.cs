using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Notification;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Diagnostics;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : AbstractHandler
{
    private readonly NotificationAdminHub _adminHub;
    private readonly ActivityPublisher _activityPublisher;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;

    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;

    public ConfirmOfferHandler(
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher, 
        NotificationAdminHub adminHub,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions)
        : base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
        _userService = userService;
        _adminHub = adminHub;
        _activityPublisher = activityPublisher;
        _activityService = activityService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var nextState = StatesEnum.MainMenu;
        CurrentUser.State.StateNumber = nextState;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        CurrentUser.Offer.OfferState = false;
        await _userService.UpdateUser(CurrentUser);
        await _adminHub.Send(JsonConvert.SerializeObject(CurrentUser.Offer));

        var offerConfirmResponse = new ResponseMessage
        {
            Text = "Поздравляю! Твоя активность создана. Она будет опубликована после проверки",
            Keyboard = Keyboards.GetEmptyKeyboard()
        };
        
        await _activityPublisher.SendMessageAsync(userData.ChatId, offerConfirmResponse);

        await Task.Delay(3000);

        var newActivityResponse = new ResponseMessage
        {
            Text = CurrentUser.Offer.GetActivityDescription().ToString(),
            Image = CurrentUser.Offer.Image,
            Keyboard = InlineKeyboardMarkup.Empty()
        };

        var tgMessage = await _activityPublisher.SendMessageAsync(_botConfig.TelegramChannel, newActivityResponse);

        await _activityService.PublishActivity(CurrentUser.Offer, tgMessage.MessageId);
            
        Response.Text = $"{CurrentUser.State}";
        Response.Image = await GetImage(nextState.ToString());
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();

        CurrentUser.Offer = null;
    }

    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}