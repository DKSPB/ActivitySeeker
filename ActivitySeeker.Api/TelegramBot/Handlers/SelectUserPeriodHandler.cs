using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.UserPeriod)]
    public class SelectUserPeriodHandler: AbstractHandler
    {
        public SelectUserPeriodHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
            : base(userService, activityService, activityPublisher)
        { }

        protected override Task ActionsAsync(UserUpdate userData)
        {
            CurrentUser.State.StateNumber = StatesEnum.PeriodFromDate;
            
            Response.Text = $"Введите дату, с которой хотите искать активности в форматах:" +
                                  $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                                  $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            return Task.CompletedTask;
        }
    }
}
