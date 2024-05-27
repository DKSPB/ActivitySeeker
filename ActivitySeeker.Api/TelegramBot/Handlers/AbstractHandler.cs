using ActivitySeeker.Bll.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public abstract class AbstractHandler
{
    private IUserService UserService { get; set; }
    protected IActivityService ActivityService { get; set; }
    protected string ResponseMessageText { get; init; } = default!;
    private ITelegramBotClient BotClient { get; set; }
    protected User CurrentUser { get; private set; } = default!;
    
    protected AbstractHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService)
    {
        BotClient = botClient;
        UserService = userService;
        ActivityService = activityService;
    }
    public async Task HandleAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        CurrentUser = GetCurrentUser(callbackQuery);
        
        await BotClient.AnswerCallbackQueryAsync(callbackQuery.Id, cancellationToken: cancellationToken);

        await BotClient.EditMessageReplyMarkupAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: CurrentUser.MessageId,
            replyMarkup: InlineKeyboardMarkup.Empty(),
            cancellationToken
        );
        
        await ActionsAsync(callbackQuery, cancellationToken);
        
        var message = await BotClient.SendTextMessageAsync(
            callbackQuery.Message.Chat.Id,
            text: ResponseMessageText,
            replyMarkup: GetKeyboard(),
            cancellationToken: cancellationToken);

        CurrentUser.MessageId = message.MessageId;
        UserService.CreateOrUpdateUser(CurrentUser);
    }

    private User GetCurrentUser(CallbackQuery callbackQuery)
    {
        var currentUserId = callbackQuery.From.Id;
        
        return UserService.GetUserById(currentUserId);
    }

    protected abstract Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken);

    protected abstract InlineKeyboardMarkup GetKeyboard();
}