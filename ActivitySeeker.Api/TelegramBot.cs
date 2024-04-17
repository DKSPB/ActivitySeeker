using Telegram.Bot;

namespace ActivitySeeker.Api;

public class TelegramBot
{
    private readonly IConfiguration _configuration;

    public TelegramBot(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<TelegramBotClient> GetBot()
    {
        string botToken = GetBotToken();
        string pathToApi = "api/message";
        string webhook = string.Concat(GetWebhookUrl(), pathToApi);
        
        TelegramBotClient botClient = new TelegramBotClient(botToken);
        await botClient.SetWebhookAsync(webhook);

        return botClient;
    }

    private string GetBotToken()
    {
        string botToken = _configuration["BotToken"];

        if (string.IsNullOrEmpty(botToken))
        {
            throw new ArgumentNullException(
                $"Не удалось получить токен телеграм-бота. Убедитесь, что в файле appsettings.json существует раздел \"BotToken\"");
        }

        return botToken;
    }
    
    private string GetWebhookUrl()
    {
        string url = _configuration["WebhookUrl"];

        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(
                $"Не удалось получить wenhook. Убедитесь, что в файле appsettings.json существует раздел \"WebhookUrl\"");
        }

        return url;
    }
}