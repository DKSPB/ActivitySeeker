using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

[HandlerState(StatesEnum.AddOfferDescription)]
public class AddOfferDescriptionHandler : AbstractHandler
{
    public AddOfferDescriptionHandler(ITelegramBotClient botClient, IUserService userService,
        IActivityService activityService)
        : base(botClient, userService, activityService)
    {
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.SaveOfferDescription;
        
        ResponseMessageText = $"Введите описание предложенного мероприятия: ";
        
        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return InlineKeyboardMarkup.Empty();
    }
}