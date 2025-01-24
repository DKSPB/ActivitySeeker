using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.SaveActivityFormat)]
    public class SaveActivityFormatHandler : AbstractHandler
    {
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
               
               Response.Text = CurrentUser.State.ToString();
               Response.Keyboard = Keyboards.GetMainMenuKeyboard();
           }
           else if (userData.Data.Equals("offline"))
           {
               CurrentUser.State.ActivityFormat = false;
               CurrentUser.State.StateNumber = StatesEnum.MainMenu;
               
               Response.Text = CurrentUser.State.ToString();
               Response.Keyboard = Keyboards.GetMainMenuKeyboard();
           }

           else if (userData.Data.Equals("any"))
           {
               CurrentUser.State.ActivityFormat = null; 
               CurrentUser.State.StateNumber = StatesEnum.MainMenu;
               
               Response.Text = CurrentUser.State.ToString();
               Response.Keyboard = Keyboards.GetMainMenuKeyboard();
           }
           else
           {
               Response.Text = "Выберите формат проведения активности:";
               Response.Keyboard = Keyboards.GetActivityFormatsKeyboard(true);
           }
           return Task.CompletedTask;
        }
    }
}
