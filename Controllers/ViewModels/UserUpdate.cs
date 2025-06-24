namespace Controllers.ViewModels
{
    public class UserUpdate
    {
        public long ChatId {  get; init; }

        public int MessageId { get; init; }

        public string? CallbackQueryId { get; init; }

        public long TelegramUserId { get; init; }

        public string? TelegramUsername { get; init; }

        public string Data { get; init; } = string.Empty;
    }
}
