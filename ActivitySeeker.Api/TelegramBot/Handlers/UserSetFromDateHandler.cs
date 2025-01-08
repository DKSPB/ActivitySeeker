using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodFromDate)]
public class UserSetFromDateHandler : AbstractHandler
{
    private readonly IUserService _userService;
    private readonly ActivityPublisher _activityPublisher;

    public UserSetFromDateHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    {
        _userService = userService;
        _activityPublisher = activityPublisher;
    }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        var fromDateText = userData.Data;

        var result = DateParser.ParseDate(fromDateText, out var fromDate) ||
                     DateParser.ParseDateTime(fromDateText, out fromDate);

        if (result)
        {
            ResponseMessageText = $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            CurrentUser.State.SearchFrom = fromDate;
            CurrentUser.State.StateNumber = StatesEnum.PeriodToDate;
        }
        else
        {
            ResponseMessageText = $"Введёная дата не соответствует форматам:" +
              $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
              $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
        }

        return Task.CompletedTask;
    }
}