using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.Controllers;

public abstract class AbstractHandler
{
    private IUserService UserService { get; set; }
    private ITelegramBotClient BotClient { get; set; }
    private CancellationToken CancellationToken { get; set; }
    protected User CurrentUser { get; set; }
    
    protected AbstractHandler(ITelegramBotClient botClient, IUserService userService,
        CancellationToken cancellationToken, User currentUser)
    {
        UserService = userService;
        BotClient = botClient;
        CancellationToken = cancellationToken;
        CurrentUser = currentUser;

    }
    public async Task HandleAsync(CallbackQuery callbackQuery,  InlineKeyboardMarkup keyboard, string messageText)
    {
        await BotClient.AnswerCallbackQueryAsync(
            callbackQuery.Id, cancellationToken: CancellationToken);

        await BotClient.EditMessageReplyMarkupAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: CurrentUser.MessageId,
            replyMarkup: InlineKeyboardMarkup.Empty(),
            CancellationToken
        );
        
        await ActionsAsync(callbackQuery);
        
        var message = await BotClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            text: messageText,
            replyMarkup: keyboard,
            cancellationToken: CancellationToken);

        CurrentUser.MessageId = message.MessageId;
        UserService.CreateOrUpdateUser(CurrentUser);
    }

    protected abstract Task ActionsAsync(CallbackQuery callbackQuery);
}