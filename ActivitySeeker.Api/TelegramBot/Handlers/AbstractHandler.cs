using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public abstract class AbstractHandler
{
    private IUserService UserService { get; set; }
    protected IActivityService ActivityService { get; set; }
    protected string ResponseMessageText { get; set; } = default!;
    protected ITelegramBotClient BotClient { get; set; }
    protected UserDto CurrentUser { get; private set; } = default!;
    
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

        try
        {
            await EditPreviousMessage(callbackQuery, cancellationToken);
        }
        finally
        {
            await ActionsAsync(callbackQuery, cancellationToken);

            if (callbackQuery.Message is null)
            {
                throw new ArgumentNullException(nameof(callbackQuery.Message));
            }

            var message = await BotClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                text: ResponseMessageText,
                replyMarkup: GetKeyboard(),
                cancellationToken: cancellationToken);

            CurrentUser.MessageId = message.MessageId;
            UserService.CreateOrUpdateUser(CurrentUser);
        }   

    }

    private UserDto GetCurrentUser(CallbackQuery callbackQuery)
    {
        var currentUserId = callbackQuery.From.Id;
        
        return UserService.GetUserById(currentUserId);
    }

    protected abstract Task ActionsAsync(CallbackQuery callbackQuery, CancellationToken cancellationToken);

    protected abstract InlineKeyboardMarkup GetKeyboard();

    protected virtual async Task EditPreviousMessage(CallbackQuery callbackQuery, CancellationToken cancellationToken)
    {
        await BotClient.EditMessageReplyMarkupAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: CurrentUser.MessageId,
            replyMarkup: InlineKeyboardMarkup.Empty(),
            cancellationToken
        );
    }
}