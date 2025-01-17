using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.MainMenu)]
public class MainMenuHandler: AbstractHandler
{
    private const string MessageText = "Выбери тип активности и время проведения:";

    private readonly ActivityPublisher _activityPublisher;
    private readonly ISettingsService _settingsService;
    public MainMenuHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher, ISettingsService settingsService) :
        base(userService, activityService, activityPublisher)
    {
        _settingsService = settingsService;
        _activityPublisher = activityPublisher;
        ResponseMessageText = MessageText;
    }
    protected override Task ActionsAsync(UserUpdate userUpdate)
    {
        ResponseMessageText = CurrentUser.State.ToString();
        Keyboard = Keyboards.GetMainMenuKeyboard();
        return Task.CompletedTask;
    }
    
    protected override async Task<Message> SendMessageAsync(long chatId)
    {

        var imgData = await _settingsService.GetImage("");
        return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, imgData, Keyboards.GetActivityPaginationKeyboard());
    }
}