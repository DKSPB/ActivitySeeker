using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Notification;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ConfirmOffer)]
public class ConfirmOfferHandler : AbstractHandler
{
    private readonly NotificationAdminHub _adminHub;
    private readonly ActivityPublisher _activityPublisher;
    private readonly IUserService _userService;

    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

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
        _rootImageFolder = botConfigOptions.Value.RootImageFolder;
        _userService = userService;
        _adminHub = adminHub;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;

        if (CurrentUser.Offer is null)
        {
            throw new NullReferenceException("Предложенная активность не может быть null");
        }

        CurrentUser.Offer.OfferState = false;
        _userService.UpdateUser(CurrentUser);
        await _adminHub.Send(JsonConvert.SerializeObject(CurrentUser.Offer));

        var offerConfirmResponse = new ResponseMessage
        {
            Text = "Поздравляю! Твоя активность создана. Она будет опубликована после проверки",
            Keyboard = Keyboards.GetEmptyKeyboard()
        };
        
        await _activityPublisher.SendMessageAsync(userData.ChatId, offerConfirmResponse);

        var mainMenuState = new MainMenu(_rootImageFolder, _webRootPath);
        Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());

        CurrentUser.Offer = null;
    }
}