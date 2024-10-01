using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class AddOfferPictureHandler : AbstractHandler
{
    public AddOfferPictureHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService) : base(botClient, userService, activityService)
    {
    }

    protected override Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser.State.StateNumber = StatesEnum.SaveOfferPicture;

        ResponseMessageText = $"Приложите картинку в формате PNG или JPEG:";
        
        return Task.CompletedTask;
    }

    protected override IReplyMarkup GetKeyboard()
    {
        return InlineKeyboardMarkup.Empty();
    }
}