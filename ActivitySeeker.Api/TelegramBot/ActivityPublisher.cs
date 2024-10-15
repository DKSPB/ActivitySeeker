using Microsoft.AspNetCore.Components.Forms;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot
{
    public class ActivityPublisher
    {
        private readonly ITelegramBotClient _botClient;

        public ActivityPublisher(ITelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task PublishActivity(string postText, byte[]? postImage, CancellationToken cancellationToken)
        {
            if (postImage is null)
            {
                await _botClient.SendTextMessageAsync(
                    chatId: "@activity_seeker_channel", 
                    text: postText, 
                    disableNotification: true, 
                    cancellationToken: cancellationToken);
            }
            else
            {
                await _botClient.SendPhotoAsync(
                    chatId: "@activity_seeker_channel",
                    photo: new InputFileStream(new MemoryStream(postImage)),
                    caption: postText,
                    disableNotification: true,
                    cancellationToken: cancellationToken);
            }
        }
    }
}
