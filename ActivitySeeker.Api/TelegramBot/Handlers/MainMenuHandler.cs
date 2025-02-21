using ActivitySeeker.Api.Models;
using ActivitySeeker.Api.States;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(/*StatesEnum.MainMenu*/"/main_menu")]
public class MainMenuHandler: AbstractHandler
{
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    public MainMenuHandler (
        IUserService userService, 
        IActivityService activityService, 
        ActivityPublisher activityPublisher,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) :
        base (userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _botConfig = botConfigOptions.Value;
    }
    protected override async Task ActionsAsync(UserUpdate userUpdate)
    {
        var nextState = StatesEnum.MainMenu;
        CurrentUser.State.StateNumber = nextState;

        var mainMenu = new MainMenu(_botConfig.RootImageFolder, _webRootPath);
        Response = await mainMenu.GetResponseMessage(CurrentUser.State.ToString());

        /*Response.Text = CurrentUser.State.ToString();
        Response.Image = await GetImage(nextState.ToString());
        Response.Keyboard = Keyboards.GetMainMenuKeyboard();*/
    }

    /*private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }*/
}