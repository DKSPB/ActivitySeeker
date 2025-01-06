using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ListOfActivities)]
public class ListOfActivitiesHandler: AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private List<ActivityTypeDto> _childrenTypes;

    private InlineKeyboardMarkup _keyboard = Keyboards.GetMainMenuKeyboard();

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
            ResponseMessageText = "Не найден заданный тип активности. Выберите тип активности из приведённых.";
            _keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes);
        }
        else
        {
            if (selectedActivityId == Guid.Empty)
            {
                CurrentUser.State.ActivityType = new();
                
                ResponseMessageText = CurrentUser.State.ToString();
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
                    CurrentUser.State.StateNumber = StatesEnum.MainMenu;
                }
                else
                {
                    ResponseMessageText = "Выбери тип активности:";
                    _childrenTypes.Add(new ActivityTypeDto { Id = selectedActivityType.Id, TypeName = "Далее" });
                    _keyboard = Keyboards.GetActivityTypesKeyboard(_childrenTypes.ToList());
                }
            }
        }
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return _keyboard;
    }
}