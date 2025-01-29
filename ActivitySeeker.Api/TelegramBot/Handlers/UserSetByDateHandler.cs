using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Utils;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodToDate)]
public class UserSetByDateHandler : AbstractHandler
{
    private readonly string _webRootPath;
    private readonly string _rootImageFolder;

    public UserSetByDateHandler(
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
        var byDateText = userData.Data;

        var result = DateParser.ParseDate(byDateText, out var byDate) || 
                     DateParser.ParseDateTime(byDateText, out byDate);

        if (result)
        {
            var compareResult = DateTime.Compare(byDate, CurrentUser.State.SearchFrom.GetValueOrDefault());

            if (compareResult <= 0)
            {
                Response.Text = $"Дата окончания поиска должа быть позднее, чем дата начала поиска";
                Response.Keyboard = Keyboards.GetEmptyKeyboard();
            }
            else
            {
                var nextState = StatesEnum.MainMenu;
                CurrentUser.State.StateNumber = nextState;
                
                CurrentUser.State.SearchTo = byDate;

                Response.Text = CurrentUser.State.ToString();
                Response.Keyboard = Keyboards.GetMainMenuKeyboard();
                Response.Image = await GetImage(nextState.ToString());
            }
        }
        else
        {
            Response.Text = $"Введёная дата не соответствует форматам:" +
                          $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                          $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            Response.Keyboard = Keyboards.GetEmptyKeyboard();
        }
    }
    
    private async Task<byte[]?> GetImage(string fileName)
    {
        var filePath = FileProvider.CombinePathToFile(_webRootPath, _rootImageFolder, fileName);

        return await FileProvider.GetImage(filePath);
    }
}