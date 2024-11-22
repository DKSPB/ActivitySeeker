using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SaveActivityFormat)]
    public class SaveActivityFormatHandler : AbstractHandler
    {
        private InlineKeyboardMarkup _keyboard;
        public SaveActivityFormatHandler(IUserService userService, 
            IActivityService activityService, ActivityPublisher activityPublisher) 
            : base(userService, activityService, activityPublisher)
        {}

        protected override Task ActionsAsync(UserUpdate userData)
        {
           if (userData.Data.Equals("online"))
           {
               CurrentUser.State.ActivityFormat = true;
               CurrentUser.State.StateNumber = StatesEnum.MainMenu;
               ResponseMessageText = CurrentUser.State.ToString();
               _keyboard = Keyboards.GetMainMenuKeyboard();
           }
           else if (userData.Data.Equals("offline"))
           {
               CurrentUser.State.ActivityFormat = false;
               CurrentUser.State.StateNumber = StatesEnum.MainMenu;
               ResponseMessageText = CurrentUser.State.ToString();
               _keyboard = Keyboards.GetMainMenuKeyboard();
           }

           else if (userData.Data.Equals("any"))
           {
               CurrentUser.State.ActivityFormat = null; 
               CurrentUser.State.StateNumber = StatesEnum.MainMenu;
               ResponseMessageText = CurrentUser.State.ToString();
               _keyboard = Keyboards.GetMainMenuKeyboard();
           }
           else
           {
               ResponseMessageText = "Выберите формат проведения активности:";
               _keyboard = Keyboards.GetActivityFormatsKeyboard(true);
           }
           return Task.CompletedTask;
        }

        protected override InlineKeyboardMarkup GetKeyboard()
        {
            return _keyboard;
        }
    }
}
