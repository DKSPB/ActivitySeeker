using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using Microsoft.Extensions.Options;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.TodayPeriod)]
public class SelectTodayPeriodHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

    public SelectTodayPeriodHandler(
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
        
        CurrentUser.State.SearchFrom = DateTime.Now;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(1).Date;

        var mainMenuState = new MainMenu(_rootImageFolder, _webRootPath);
        Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
    }
}