using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.TomorrowPeriod)]
public class SelectTomorrowPeriodHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;
    public SelectTomorrowPeriodHandler(
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
        var nextState = StatesEnum.MainMenu;
        CurrentUser.State.StateNumber = nextState;
        
        CurrentUser.State.SearchFrom = DateTime.Now.AddDays(1).Date;
        CurrentUser.State.SearchTo = DateTime.Now.AddDays(2).Date;
        
        Response.Text = CurrentUser.State.ToString();
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();
        Response.Image = await GetImage(nextState.ToString());

    }
    
    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _rootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}