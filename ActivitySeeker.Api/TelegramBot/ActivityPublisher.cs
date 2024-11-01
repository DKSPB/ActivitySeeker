using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot
{
    public class ActivityPublisher
    {
        private readonly ITelegramBotClient _botClient;

        public ActivityPublisher(ITelegramBotClient botClient)
        {
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
            else
            {
                return await _botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(postImage)),
                    caption: postText,
                    disableNotification: true,
                    replyMarkup: replyMarkup);
            }
        }

        public async Task<Message> EditMessageAsync(ChatId chatId, int messageId, InlineKeyboardMarkup replyMarkup)
        {
            return await _botClient.EditMessageReplyMarkupAsync(
                chatId: chatId,
                messageId: messageId,
                replyMarkup: replyMarkup);
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
