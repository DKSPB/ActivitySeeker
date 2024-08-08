using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using System.Globalization;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.UserPeriod)]
    public class SelectUserPeriodHandler: AbstractHandler
    {
        public SelectUserPeriodHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) 
            : base(botClient, userService, activityService)
        { }

        protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
        {
            CurrentUser.State.StateNumber = StatesEnum.PeriodFromDate;
            
            ResponseMessageText = $"Введите дату, с которой хотите искать активностив форматах:" +
                                  $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                                  $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            return Task.CompletedTask;
        }

        protected override IReplyMarkup GetKeyboard()
        {
            return InlineKeyboardMarkup.Empty();
        }
    }
}
