using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.WeekPeriod*/"/week_period")]
public class SelectWeekPeriodHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

    public SelectWeekPeriodHandler(
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
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(7).Date;

        var mainMenuState = new MainMenu(_rootImageFolder, _webRootPath);
        Response = await mainMenuState.GetResponseMessage(CurrentUser.State.ToString());
    }
}