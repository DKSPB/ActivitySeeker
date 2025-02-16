using ActivitySeeker.Api.Models;
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

        public async Task<Message> SendMessageAsync(ChatId chatId, ResponseMessage response)
        {
            if (response.Image is null)
            {
                return await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: response.Text, 
                    disableNotification: true,
                    replyMarkup: response.Keyboard);
            }

            return await _botClient.SendPhotoAsync(
                chatId: chatId,
                photo: new InputFileStream(new MemoryStream(response.Image)),
                caption: response.Text,
                disableNotification: true,
                replyMarkup: response.Keyboard);
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
