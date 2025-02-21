using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using Microsoft.Extensions.Options;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;


namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.AfterTomorrowPeriod*/"/after_tomorrow_period")]
public class SelectAfterTomorrowPeriodHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

    public SelectAfterTomorrowPeriodHandler(
        IUserService userService,
        IActivityService activityService,
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions)
        : base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _rootImageFolder = botConfigOptions.Value.RootImageFolder;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.MainMenu;
        
        CurrentUser.State.SearchFrom = DateTime.Now.AddDays(2).Date;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(3).Date;

        var mainMenuState = new MainMenu(_rootImageFolder, _webRootPath);
        Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
    }
}