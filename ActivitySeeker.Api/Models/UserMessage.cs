namespace ActivitySeeker.Api.Models
{
    public class UserMessage
    {
        public long ChatId {  get; set; }

        public int MessageId { get; set; }

        public string? CallbackQueryId { get; set; }

        public long TelegramUserId { get; set; }

        public string? TelegramUsername { get; set; }

        public string Data { get; set; } = string.Empty;
    }
}
