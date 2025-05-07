namespace Controllers.Api.Models;

public class ActivitySeekerConfig
{
    public string BotToken { get; set; } = default!;

    public string WebhookUrl { get; set; } = default!;

    public string? PathToCertificate { get; set; }

    public string TelegramChannel { get; set; } = default!;

    public string RootImageFolder { get; set; } = default!;

    public long MaxFileSize { get; set; }
}