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

        public async Task<Message> PublishActivity(ChatId chatId, string postText, byte[]? postImage, IReplyMarkup replyMarkup, CancellationToken cancellationToken)
        {
            if (postImage is null)
            {
                return await _botClient.SendTextMessageAsync(
                    chatId: chatId,//"@activity_seeker_channel", 
                    text: postText, 
                    disableNotification: true,
                    replyMarkup: replyMarkup,
                    cancellationToken: cancellationToken);
            }
            else
            {
                return await _botClient.SendPhotoAsync(
                    chatId: chatId,//"@activity_seeker_channel",
                    photo: new InputFileStream(new MemoryStream(postImage)),
                    caption: postText,
                    disableNotification: true,
                    replyMarkup: replyMarkup,
                    cancellationToken: cancellationToken);
            }
        }
    }
}
