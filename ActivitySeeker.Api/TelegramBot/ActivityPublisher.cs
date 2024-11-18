using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot
{
    public class ActivityPublisher
    {
        private readonly ILogger<ActivityPublisher> _logger;
        private readonly ITelegramBotClient _botClient;

        public ActivityPublisher(ILogger<ActivityPublisher> logger, ITelegramBotClient botClient)
        {
            _logger = logger;
            _botClient = botClient;
        }

        public async Task<Message> SendMessageAsync(ChatId chatId, string postText, byte[]? postImage = null, IReplyMarkup? replyMarkup = null)
        {
            if (postImage is null)
            {
                return await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: postText, 
                    disableNotification: true,
                    replyMarkup: replyMarkup);
            }

            return await _botClient.SendPhotoAsync(
                chatId: chatId,
                photo: new InputFileStream(new MemoryStream(postImage)),
                caption: postText,
                disableNotification: true,
                replyMarkup: replyMarkup);
        }

        public async Task EditMessageAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup)
        {
            try
            {
                await _botClient.EditMessageReplyMarkupAsync(
                    chatId: chatId,
                    messageId: messageId,
                    replyMarkup: replyMarkup);
            }
            catch (Exception)
            {}

        }

        public async Task DeleteMessage(ChatId chatId, int messageId)
        {
            await _botClient.DeleteMessageAsync(
                chatId,
                messageId);
        }

        public async Task AnswerOnPushButton(string callbackQueryId)
        {
            await _botClient.AnswerCallbackQueryAsync(callbackQueryId);
        }
    }
}
