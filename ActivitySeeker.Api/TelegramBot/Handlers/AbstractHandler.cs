using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public abstract class AbstractHandler: IHandler
{
    protected IUserService UserService { get; set; }
    protected IActivityService ActivityService { get; set; }
    protected string ResponseMessageText { get; set; } = default!;
    protected ITelegramBotClient BotClient { get; set; }
    protected UserDto CurrentUser { get; private set; } = default!;

    private readonly ActivityPublisher _activityPublisher;
    
    protected AbstractHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
    {
        BotClient = botClient;
        UserService = userService;
        ActivityService = activityService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, Update update)
    {
        var callbackQuery = update.CallbackQuery;
        CurrentUser = currentUser;

        await BotClient.AnswerCallbackQueryAsync(callbackQuery.Id);

        try
        {
            await EditPreviousMessage(callbackQuery);
        }
        finally
        {
            await ActionsAsync(callbackQuery);

            if (callbackQuery.Message is null)
            {
                throw new ArgumentNullException(nameof(callbackQuery.Message));
            }

            var message = await SendMessageAsync(callbackQuery.Message.Chat.Id);

            CurrentUser.State.MessageId = message.MessageId;
            UserService.UpdateUser(CurrentUser);
        }   

    }

    protected abstract Task ActionsAsync(CallbackQuery callbackQuery);

    protected abstract IReplyMarkup GetKeyboard();

    protected virtual async Task EditPreviousMessage(CallbackQuery callbackQuery)
    {
        await _activityPublisher.EditMessageAsync(
            chatId: callbackQuery.Message.Chat.Id,
            messageId: CurrentUser.State.MessageId,
            replyMarkup: InlineKeyboardMarkup.Empty());
    }

    protected virtual async Task<Message> SendMessageAsync(long chatId)
    {
        return await _activityPublisher.PublishActivity(chatId, ResponseMessageText, null, GetKeyboard());
    }
}