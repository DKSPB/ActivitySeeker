using System.Text;
using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SelectActivityFormat)]
    public class SelectActivityFormat : AbstractHandler
    {
        public SelectActivityFormat(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher) 
            : base(userService, activityService, activityPublisher)
        {}

        protected override Task ActionsAsync(UserUpdate userData)
        {
            ResponseMessageText = "Выбери формат проведения активности:";
            CurrentUser.State.StateNumber = StatesEnum.SaveActivityFormat;

            return Task.CompletedTask;
        }

        protected override InlineKeyboardMarkup GetKeyboard()
        {
            return Keyboards.GetActivityFormatsKeyboard(true);
        }
    }
}
