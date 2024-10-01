using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDate)]
public class AddOfferDateHandler : AbstractHandler
{
    public AddOfferDateHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : base(botClient, userService, activityService)
    {
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.SaveOfferDate;

        ResponseMessageText = $"Введите дату мероприятия в формате (дд.мм.гггг чч.мм):" +
                              $"\nпример:{DateTime.Now:dd.MM.yyyy HH:mm}";
        
        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return InlineKeyboardMarkup.Empty();
    }
}