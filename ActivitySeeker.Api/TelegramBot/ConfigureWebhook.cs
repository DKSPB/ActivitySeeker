using Microsoft.Extensions.Options;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ActivitySeeker.Api.TelegramBot;

public class ConfigureWebhook : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly BotConfiguration _botConfig;

    public ConfigureWebhook(IServiceProvider serviceProvider, IOptions<BotConfiguration> botOptions)
    {
        _serviceProvider = serviceProvider;
        _botConfig = botOptions.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        var pathToCertificate = _botConfig.PathToCertificate;

        InputFileStream? fileStream = null;

        if (!string.IsNullOrEmpty(pathToCertificate)) 
        {
            if (System.IO.File.Exists(pathToCertificate))
            {
                fileStream = new(System.IO.File.OpenRead(pathToCertificate));
            }
        }

        var webhookAddress = $"{_botConfig.WebhookUrl}/api/message";
        await botClient.SetWebhookAsync(
            url: webhookAddress,
            certificate: fileStream,
            allowedUpdates: Array.Empty<UpdateType>(),
            cancellationToken: cancellationToken);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var botClient = scope.ServiceProvider.GetRequiredService<ITelegramBotClient>();

        await botClient.DeleteWebhookAsync(cancellationToken: cancellationToken);
    }
}