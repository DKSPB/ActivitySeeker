using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.PeriodToDate)]
public class UserSetByDateHandler: AbstractHandler
{
    public UserSetByDateHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
        :base(userService, activityService, activityPublisher)
    { }

    protected override Task ActionsAsync(UserUpdate userData)
    {
        var byDateText = userData.Data;

        var result = DateParser.ParseDate(byDateText, out var byDate) ||
                     DateParser.ParseDateTime(byDateText, out byDate);

        if (result)
        {
            var compareResult = DateTime.Compare(byDate, CurrentUser.State.SearchFrom.GetValueOrDefault());

            if (compareResult <= 0)
            {
                ResponseMessageText = $"Дата окончания поиска должа быть позднее, чем дата начала поиска";

                Keyboard = Keyboards.GetEmptyKeyboard();
            }
            else
            {
                CurrentUser.State.SearchTo = byDate;

                ResponseMessageText = CurrentUser.State.ToString();

                Keyboard = Keyboards.GetMainMenuKeyboard();
            }
        }
        else
        {
            ResponseMessageText = $"Введёная дата не соответствует форматам:" +
                          $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                          $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";

            Keyboard = Keyboards.GetEmptyKeyboard();
        }

        return Task.CompletedTask;
    }
}