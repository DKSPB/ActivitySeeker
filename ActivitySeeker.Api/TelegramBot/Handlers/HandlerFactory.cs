namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class HandlerFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public HandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public AbstractHandler GetHandler(string callbackData)
    {
        if (callbackData.Equals("mainMenu"))
        {
            return _serviceProvider.GetRequiredService<MainMenuHandler>();
        }
        if (callbackData.Equals("selectActivityTypeButton"))
        {
            return _serviceProvider.GetRequiredService<SelectActivityTypeHandler>();
        }

        if (callbackData.Contains("activityType"))
        {
            return _serviceProvider.GetRequiredService<ListOfActivitiesHandler>();
        }

        throw new ArgumentException("Unrecognized handler");
    }
}