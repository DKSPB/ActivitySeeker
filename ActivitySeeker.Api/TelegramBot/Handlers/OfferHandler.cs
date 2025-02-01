using ActivitySeeker.Bll.Models;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.Offer)]
public class OfferHandler : AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;

    private IEnumerable<ActivityTypeDto>? _children;

    public OfferHandler(IUserService userService, IActivityService activityService, IActivityTypeService activityTypeService,
        ActivityPublisher activityPublisher): base(userService, activityService, activityPublisher)
    {
        _activityTypeService = activityTypeService;
    }

    protected override async Task ActionsAsync(UserUpdate userData)
    {
        CurrentUser.State.StateNumber = StatesEnum.Offer;
        var activityTypeIdParseResult = Guid.TryParse(userData.Data, out var activityTypeId);

        if (!activityTypeIdParseResult)
        {
            var activityTypes = (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
            Response.Text = "Выбери тип активности:";
            Response.Keyboard = Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName(), "Меню");
        }
        else
        {
            var activityType = await _activityTypeService.GetById(activityTypeId);

            _children = (await _activityTypeService.GetAll())
            .Where(x => x.ParentId == activityType.Id).ToList();

            if (!_children.Any())
            {
                Response.Text = "Выбери формат проведения:";
                CurrentUser.State.StateNumber = StatesEnum.SaveOfferFormat;
                Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(false);

                CreateOfferIfNotExists((Guid)activityType.Id);
            }
            else
            {
                Response.Text = "Выбери тип активности";

                if (activityType.ParentId is null)
                {
                    Response.Keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), StatesEnum.Offer.GetDisplayName());
                }
                else 
                {
                    Response.Keyboard = Keyboards.GetActivityTypesKeyboard(_children.ToList(), activityType.ParentId.ToString());
                }
                
            }
        }
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