using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot.Types.ReplyMarkups;

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
            CurrentUser.State.ActivityFormat = userData.Data switch
            {
                "any" => null,
                "online" => true,
                "offline" => false,
                _ => CurrentUser.State.ActivityFormat
            };

            CurrentUser.State.StateNumber = StatesEnum.MainMenu;
            ResponseMessageText = CurrentUser.State.ToString();

            return Task.CompletedTask;
        }

        protected override InlineKeyboardMarkup GetKeyboard()
        {
            return Keyboards.GetMainMenuKeyboard();
        }
    }
}
