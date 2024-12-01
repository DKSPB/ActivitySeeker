using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private IEnumerable<ActivityTypeDto> _children;

    private InlineKeyboardMarkup _keyboard; 

    public AddOfferDescriptionHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService, ActivityPublisher activityPublisher)
        : base(userService, activityService, activityPublisher)
    {
        _activityTypeService = activityTypeService;
        _children = new List<ActivityTypeDto>();
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var activityTypeId);

        if (!activityTypeIdParseResult)
        {
            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            ResponseMessageText = "Не найден заданный тип активности. Выберите тип активности из приведённых.";
            _keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, "К поиску активностей");
        }
        else
        {
            _children = (await _activityTypeService.GetAll())
                .Where(x => x.ParentId == activityTypeId).ToList();

            if (!_children.Any())
            {
                ResponseMessageText = "Выбери формат проведения:";
                CurrentUser.State.StateNumber = StatesEnum.SaveOfferFormat;
                _keyboard = Keyboards.GetActivityFormatsKeyboard(false);
            
                CreateOfferIfNotExists(activityTypeId);
            }
            else
            {
                ResponseMessageText = "Выбери тип активности";
                _keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList());
            }
        }
    }
    
    protected override IReplyMarkup GetKeyboard()
    {
        return _keyboard;
    }
    private void CreateOfferIfNotExists(Guid activityTypeId)
    {
        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = activityTypeId,
                LinkOrDescription = string.Empty,
                OfferState = false,
                IsOnline = false
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = activityTypeId;
        }
    }
}