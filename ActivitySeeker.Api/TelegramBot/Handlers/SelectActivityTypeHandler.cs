using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityTypeChapter)]
public class SelectActivityTypeHandler : AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;

    public SelectActivityTypeHandler(IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher,
        IActivityTypeService activityTypeService) :
        base(userService, activityService, activityPublisher)
    {
        _activityTypeService = activityTypeService;
    }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;
        
        var activityTypes = GetRootActivityTypes().Result;
        activityTypes.Insert(0, new ActivityTypeDto{Id = Guid.Empty, TypeName = "Все виды активностей"});
        
        ResponseMessageText = "Выбери тип активности:";
        Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName());
        
        return Task.CompletedTask;
    }

    private async Task<List<ActivityTypeDto>> GetRootActivityTypes()
    {
        return (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
    }
}