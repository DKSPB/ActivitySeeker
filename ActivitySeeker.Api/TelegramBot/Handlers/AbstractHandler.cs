using ActivitySeeker.Api.Models;
using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public abstract class AbstractHandler: IHandler
{
    private IUserService UserService { get; set; }
    protected IActivityService ActivityService { get; set; }
    protected string ResponseMessageText { get; set; } = default!;

    protected InlineKeyboardMarkup Keyboard { get; set; } = default!;
    protected UserDto CurrentUser { get; private set; } = default!;

    private readonly ActivityPublisher _activityPublisher;
    
    protected AbstractHandler(IUserService userService, IActivityService activityService, ActivityPublisher activityPublisher)
    {
        UserService = userService;
        ActivityService = activityService;
        _activityPublisher = activityPublisher;
    }
    public async Task HandleAsync(UserDto currentUser, UserUpdate userUpdate)
    {
        CurrentUser = currentUser;

        try
        {
            await EditPreviousMessage(userUpdate.ChatId);
        }
        finally
        {
            await ActionsAsync(userUpdate);

            if (userUpdate.Data is null)
            {
                throw new ArgumentNullException(nameof(userUpdate.Data));
            }

            var message = await SendMessageAsync(userUpdate.ChatId);

            CurrentUser.State.MessageId = message.MessageId;
            UserService.UpdateUser(CurrentUser);
        }
    }

    protected abstract Task ActionsAsync(UserUpdate userData);

    protected virtual async Task EditPreviousMessage(ChatId chatId)
    {
        await _activityPublisher.EditMessageAsync(
            chatId: chatId,
            messageId: CurrentUser.State.MessageId,
            replyMarkup: InlineKeyboardMarkup.Empty());
    }

    protected virtual async Task<Message> SendMessageAsync(long chatId)
    {     
        return await _activityPublisher.SendMessageAsync(chatId, ResponseMessageText, null, Keyboard);
    }
}