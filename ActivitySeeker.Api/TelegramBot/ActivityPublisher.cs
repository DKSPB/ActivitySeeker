using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot
{
    public class ActivityPublisher
    {
        private readonly ITelegramBotClient _botClient;
        private readonly CancellationToken _cancellationToken;

        public ActivityPublisher(ITelegramBotClient botClient, CancellationToken cancellationToken)
        {
            _botClient = botClient;
            _cancellationToken = cancellationToken;
        }

        public async Task<Message> PublishActivity(ChatId chatId, string postText, byte[]? postImage, IReplyMarkup replyMarkup)
        {
            if (postImage is null)
            {
                return await _botClient.SendTextMessageAsync(
                    chatId: chatId,
                    text: postText, 
                    disableNotification: true,
                    replyMarkup: replyMarkup,
                    cancellationToken: _cancellationToken);
            }
            else
            {
                return await _botClient.SendPhotoAsync(
                    chatId: chatId,
                    photo: new InputFileStream(new MemoryStream(postImage)),
                    caption: postText,
                    disableNotification: true,
                    replyMarkup: replyMarkup,
                    cancellationToken: _cancellationToken);
            }
        }
    }
}
