using ActivitySeeker.Bll.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot;

public class SendMessageStrategy
{
    private readonly ITelegramBotClient _botClient;

    private readonly long _chatId;

    private readonly InlineKeyboardMarkup _keyboard;

    private readonly CancellationToken _cancellationToken;

    public SendMessageStrategy(ITelegramBotClient botClient, long chatId, InlineKeyboardMarkup keyboard, CancellationToken cancellationToken)
    {
        _botClient = botClient;
        _chatId = chatId;
        _keyboard = keyboard;
        _cancellationToken = cancellationToken;
    }
    
    public async Task<Message> SendMessage(string messageText)
    {
        return await _botClient.SendTextMessageAsync(
            _chatId,
            text: messageText,
            replyMarkup: _keyboard,
            cancellationToken: _cancellationToken);
    }
    
    public async Task<Message> SendMessageWithSinglePhoto(byte[] image, string? caption = null)
    {
        return await _botClient.SendPhotoAsync(chatId: _chatId,
            photo: new InputFileStream(new MemoryStream(image)),
            caption: caption,
            replyMarkup: _keyboard, 
            cancellationToken: _cancellationToken);
    }

    public async Task<Message[]> SendMessageWithGroupPhoto(List<ImageDto> images, string? caption = null)
    {
        List<IAlbumInputMedia> album = new ();

        foreach (var image in images)
        {
            album.Add(new InputMediaPhoto(
                new InputFileStream(
                    new MemoryStream(image.Content), caption
                    )
                )
            );
        }

        return await _botClient.SendMediaGroupAsync(chatId: _chatId, album, cancellationToken: _cancellationToken);
    }
}