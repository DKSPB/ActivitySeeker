using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ListOfActivities)]
public class ListOfActivitiesHandler: AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private List<ActivityTypeDto> _childrenTypes;
    

    public ListOfActivitiesHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher) :
        base(userService, activityService, activityPublisher)
    {
        _childrenTypes = new List<ActivityTypeDto>();
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var selectedActivityId);

        if (!activityTypeIdParseResult)
        {
            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            activityTypes.Insert(0, new ActivityTypeDto{ Id = Guid.Empty, TypeName = "Все виды активностей" });
            ResponseMessageText = "Выбери тип активности:";
            Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName());
        }
        else
        {
            if (selectedActivityId == Guid.Empty)
            {
                CurrentUser.State.ActivityType = new();
                
                ResponseMessageText = CurrentUser.State.ToString();
                Keyboard = Keyboards.GetMainMenuKeyboard();
                
                CurrentUser.State.StateNumber = StatesEnum.MainMenu;
            }
            else
            {
                var selectedActivityType = await _activityTypeService.GetById(selectedActivityId);

                _childrenTypes = (await _activityTypeService.GetAll())
                    .Where(x => x.ParentId == selectedActivityType.Id).ToList();

                if (!_childrenTypes.Any())
                {
                    CurrentUser.State.ActivityType.Id = selectedActivityType.Id;
                    CurrentUser.State.ActivityType.TypeName = selectedActivityType.TypeName;
                
                    ResponseMessageText = CurrentUser.State.ToString();
                    Keyboard = Keyboards.GetMainMenuKeyboard();
                    
                    CurrentUser.State.StateNumber = StatesEnum.MainMenu;
                }
                else
                {
                    if(CurrentUser.State.ActivityType.Id == selectedActivityType.Id && CurrentUser.State.StateNumber == StatesEnum.ListOfChildrenActivities)
                    {
                        ResponseMessageText = CurrentUser.State.ToString();
                        Keyboard = Keyboards.GetMainMenuKeyboard();
                        
                        CurrentUser.State.StateNumber = StatesEnum.MainMenu;
                    }
                    else
                    {
                        ResponseMessageText = "Выбери тип активности:";
                        _childrenTypes.Add(new ActivityTypeDto { Id = selectedActivityType.Id, TypeName = $"Выбрать все {selectedActivityType.TypeName}" });

                        var backButtonValue = selectedActivityType.ParentId is null ? StatesEnum.ListOfActivities.GetDisplayName() : selectedActivityType.ParentId.ToString();

                        Keyboard = Keyboards.GetActivityTypesKeyboard(_childrenTypes.ToList(), backButtonValue);

                        CurrentUser.State.ActivityType.Id = selectedActivityType.Id;
                        CurrentUser.State.ActivityType.TypeName = selectedActivityType.TypeName;
                    }

                    CurrentUser.State.StateNumber = StatesEnum.ListOfChildrenActivities;
                }
            }
        }
    }
}