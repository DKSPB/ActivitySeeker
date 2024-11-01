using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Bll.Services;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.ActivityTypeChapter)]
public class SelectActivityTypeHandler: AbstractHandler
{
    private readonly IActivityTypeService _activityTypeService;

    public SelectActivityTypeHandler(/*ITelegramBotClient botClient,*/ IUserService userService,
        IActivityService activityService, ActivityPublisher activityPublisher,
        IActivityTypeService activityTypeService) :
        base(/*botClient,*/ userService, activityService, activityPublisher)
    {
        _activityTypeService = activityTypeService;
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery)
    {
        ResponseMessageText = "Выбери тип активности:";
        CurrentUser.State.StateNumber = StatesEnum.ListOfActivities;

        return Task.CompletedTask;
    }

    protected override InlineKeyboardMarkup GetKeyboard()
    {
        var activityTypes = GetRootActivityTypes().Result;
        activityTypes.Insert(0, new ActivityTypeDto{Id = Guid.Empty, TypeName = "Все виды активностей"});
        
        return Keyboards.GetActivityTypesKeyboard(activityTypes);
    }

    private async Task<List<ActivityTypeDto>> GetRootActivityTypes()
    {
        return (await _activityTypeService.GetAll()).Where(x => x.ParentId is null).ToList();
    }
}