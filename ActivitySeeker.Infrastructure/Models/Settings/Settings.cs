namespace ActivitySeeker.Infrastructure.Models.Settings;

public class Settings
{
    public string RootImageFolder { get; set; } = default!;
    public TelegramBotSettings TelegramBotSettings { get; set; } = default!;
        
    public VkBotSettings VkBotSettings { get; set; } = default!;
}

public class BotSettings
{
    public string MainMenuImageName { get; set; } = default!;
        
    public string OfferMenuImageName { get; set; } = default!;
}
    
public class TelegramBotSettings : BotSettings
{ }

public class VkBotSettings : BotSettings
{ }