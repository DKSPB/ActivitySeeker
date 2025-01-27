using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityTypeChapter)]
public class SelectActivityTypeHandler : AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private readonly string _webRootPath;
    private readonly BotConfiguration _botConfig;
    private readonly ISettingsService _settingsService;

    public SelectActivityTypeHandler(IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher,
        IActivityTypeService activityTypeService,
        ISettingsService settingsService,
        IWebHostEnvironment webHostEnvironment,
        IOptions<BotConfiguration> botConfigOptions) :
        base(userService, activityService, activityPublisher)
    {
        _webRootPath = webHostEnvironment.WebRootPath;
        _settingsService = settingsService;
        _botConfig = botConfigOptions.Value;
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;

        var activityTypes = await GetRootActivityTypes();
        activityTypes.Insert(0, new ActivityTypeDto{Id = Guid.Empty, TypeName = "Все виды активностей"});

        Response.Text = "Выбери тип активности:";
        Response.Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName());
        Response.Image = await GetImage(CurrentUser.State.StateNumber);
    }

    private async Task<List<ActivityTypeDto>> GetRootActivityTypes()
    {
        return (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
    }

    private async Task<byte[]?> GetImage(StatesEnum state)
    {
        var fileName = state.ToString();
        var filePath = _settingsService.CombinePathToFile(_webRootPath, _botConfig.RootImageFolder, fileName);

        return await _settingsService.GetImage(filePath);
    }
}