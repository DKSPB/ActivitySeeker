using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
using Telegram.Bot.Types.ReplyMarkups;

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
        ResponseMessageText = "Выбери тип активности:";
        CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;

        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        var activityTypes = GetRootActivityTypes().Result;
        activityTypes.Insert(0, new ActivityTypeDto{Id = Guid.Empty, TypeName = "Все виды активностей"});
        
        return Keyboards.GetActivityTypesKeyboard(activityTypes, StatesEnum.MainMenu.GetDisplayName());
    }

    private async Task<List<ActivityTypeDto>> GetRootActivityTypes()
    {
        return (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
    }
}