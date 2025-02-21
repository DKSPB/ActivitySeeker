using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.ActivityPeriodChapter*/"/select_activity_period")]
public class SelectActivityPeriodHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;

    public SelectActivityPeriodHandler(
        IUserService userService,
        IActivityService activityService,
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) : 
        base ( userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.ActivityPeriodChapter;

        var activityPeriodChapterState = new ActivityPeriodChapter(_botConfig.RootImageFolder, _webRootPath);
        Response = await activityPeriodChapterState.GetResponseMessage();
    }
}