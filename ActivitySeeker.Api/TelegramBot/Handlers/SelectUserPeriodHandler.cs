using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    [HandlerState(StatesEnum.UserPeriod)]
    public class SelectUserPeriodHandler: AbstractHandler
    {
        public SelectUserPeriodHandler(ITelegramBotClient botClient, IUserService userService,
            IActivityService activityService, ActivityPublisher activityPublisher) 
            : base(botClient, userService, activityService, activityPublisher)
        { }

        protected override Task ActionsAsync(CallbackQuery callbackQuery)
        {
            CurrentUser.State.StateNumber = StatesEnum.PeriodFromDate;
            
            ResponseMessageText = $"Введите дату, с которой хотите искать активности в форматах:" +
                                  $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                                  $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}";
            
            return Task.CompletedTask;
        }
    }
}
