using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : AbstractHandler
{
    private readonly IUserService _userService;
    private readonly IActivityTypeService _activityTypeService;
    private readonly ActivityPublisher _activityPublisher;

    private IEnumerable<ActivityTypeDto> _children;
    private InlineKeyboardMarkup _keyboard;

    public OfferHandler(IUserService userService, IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher): base(userService, activityService, activityPublisher)
    {
        _userService = userService;
        _activityTypeService = activityTypeService;
        _activityPublisher = activityPublisher;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.Offer;
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var activityTypeId);

        if (!activityTypeIdParseResult)
        {
            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            ResponseMessageText = "Выбери тип активности:";
            _keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName(), "Меню");
        }
        else
        {
            var activityType = await _activityTypeService.GetById(activityTypeId);

            _children = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId == activityType.Id).ToList();

            if (!_children.Any())
            {
                ResponseMessageText = "Выбери формат проведения:";
                CurrentUser.State.StateNumber = StatesEnum.SaveOfferFormat;
                _keyboard = Keyboards.GetActivityFormatsKeyboard(false);

                CreateOfferIfNotExists((Guid)activityType.Id);
            }
            else
            {
                ResponseMessageText = "Выбери тип активности";

                if (activityType.ParentId is null)
                {
                    _keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), StatesEnum.Offer.GetDisplayName());
                }
                else 
                {
                    _keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), activityType.ParentId.ToString());
                }
                
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