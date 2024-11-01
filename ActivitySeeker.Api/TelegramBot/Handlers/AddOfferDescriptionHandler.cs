using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;
    private IEnumerable<ActivityTypeDto> _children;

    public AddOfferDescriptionHandler(IUserService userService,
        IActivityService activityService, IActivityTypeService activityTypeService, ActivityPublisher activityPublisher)
        : base(userService, activityService, activityPublisher)
    {
        _activityTypeService = activityTypeService;
        _children = new List<ActivityTypeDto>();
    }

    protected override async Task ActionsAsync(CallbackQuery callbackQuery)
    {
        var activityTypeIdString = callbackQuery.Data;

        var activityTypeIdParseResult = Guid.TryParse(activityTypeIdString, out var activityTypeId);

        if (!activityTypeIdParseResult)
        {
            throw new ArgumentNullException($"Не удалось распарсить идентификатор типа активности {activityTypeIdString}");
        }

        _children = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId == activityTypeId);

        if (!_children.Any())
        {
            CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        
            ResponseMessageText = $"Заполни описание события";
            
            CreateOfferIfNotExists(activityTypeId);
        }
        else
        {
            ResponseMessageText = "Выбери тип активности";
        }
    }
    
    protected override IReplyMarkup GetKeyboard()
    {
        return CurrentUser.State.StateNumber == StatesEnum.SaveOfferDescription ? 
            Keyboards.GetEmptyKeyboard() : 
            Keyboards.GetActivityTypesKeyboard(_children.ToList());
    }
    private void CreateOfferIfNotExists(Guid activityTypeId)
    {
        if (CurrentUser.Offer is null)
        {
            CurrentUser.Offer = new ActivityDto()
            {
                ActivityTypeId = activityTypeId,
                LinkOrDescription = string.Empty,
                OfferState = false
            };
        }
        else
        {
            CurrentUser.Offer.ActivityTypeId = activityTypeId;
        }
    }
}