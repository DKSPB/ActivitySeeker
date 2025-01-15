namespace ActivitySeeker.Infrastructure.Models.Settings;

public class Settings
{
    public TelegramBotSettings? TelegramBotSettings { get; set; }
        
    public VkBotSettings? VkBotSettings { get; set; }
}

public class BotSettings
{
    public string? MainMenuImageName { get; set; }
        
    public string? OfferMenuImageName { get; set; }
}
    
public class TelegramBotSettings : BotSettings
{ }

public class VkBotSettings : BotSettings
{ }