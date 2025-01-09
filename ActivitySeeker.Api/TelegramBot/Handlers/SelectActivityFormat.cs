using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SelectActivityFormat)]
    public class SelectActivityFormat : AbstractHandler
    {
        public SelectActivityFormat(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
            : base(userService, activityService, activityPublisher) {}

        protected override Task ActionsAsync(UserUpdate userData)
        {
            ResponseMessageText = "Выбери формат проведения активности:";
            Keyboard = Keyboards.GetActivityFormatsKeyboard(true);
            
            CurrentUser.State.StateNumber = StatesEnum.SaveActivityFormat;

            return Task.CompletedTask;
        }
    }
}
