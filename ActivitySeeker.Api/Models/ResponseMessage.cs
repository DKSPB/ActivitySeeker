using ActivitySeeker.Api.TelegramBot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.Models
{
    public class ResponseMessage
    {
        public string Text { get; set; } = default!;

        public byte[]? Image { get; set; }

        public InlineKeyboardMarkup? Keyboard { get; set; } = Keyboards.GetEmptyKeyboard();
    }
}
