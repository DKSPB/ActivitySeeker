using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ListOfActivities)]
public class ListOfActivitiesHandler: AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private IEnumerable<ActivityTypeDto> _childrenTypes;

    public ListOfActivitiesHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher) :
        base(userService, activityService, activityPublisher)
    {
        _childrenTypes = new List<ActivityTypeDto>();
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery)
    {
        var activityTypeIdString = callbackQuery.Data;

        var activityTypeIdParseResult = Guid.TryParse(activityTypeIdString, out var selectedActivityId);

        if (!activityTypeIdParseResult)
        {
            throw new ArgumentNullException(
                $"Не удалось преобразовать идентификатор типа активности {activityTypeIdString} в Guid");
        }

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
                .Where(x => x.ParentId == selectedActivityType.Id);

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
            }
        }
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        return CurrentUser.State.StateNumber == StatesEnum.MainMenu ? 
            Keyboards.GetMainMenuKeyboard() : 
            Keyboards.GetActivityTypesKeyboard(_childrenTypes.ToList());
    }
}