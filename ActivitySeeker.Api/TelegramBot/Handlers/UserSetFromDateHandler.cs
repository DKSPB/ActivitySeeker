using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodFromDate)]
public class UserSetFromDateHandler : AbstractHandler
{
    public UserSetFromDateHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        var fromDateText = userData.Data;

        var result = DateParser.ParseDate(fromDateText, out var fromDate) ||
                     DateParser.ParseDateTime(fromDateText, out fromDate);

        if (result)
        {
            Response.Text = $"Введите дату, по которую хотите искать активности в форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            CurrentUser.State.SearchFrom = fromDate;
            CurrentUser.State.StateNumber = StatesEnum.PeriodToDate;
        }
        else
        {
            Response.Text = $"Введёная дата не соответствует форматам:" +
              $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
              $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
        }

        return Task.CompletedTask;
    }
}