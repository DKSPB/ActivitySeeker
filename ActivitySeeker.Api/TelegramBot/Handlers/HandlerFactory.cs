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

        if (callbackData.Equals("searchActivityButton"))
        {
            return _serviceProvider.GetRequiredService<SearchResultHandler>();
        }

        if (callbackData.Equals("back"))
        {
            return _serviceProvider.GetRequiredService<PreviousHandler>();
        }

        if (callbackData.Equals("next"))
        {
            return _serviceProvider.GetRequiredService<NextHandler>();
        }

        if (callbackData.Equals("activityStartPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectActivityPeriodHandler>();
        }

        if (callbackData.Equals("todayPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectTodayPeriodHandler>();
        }

        if (callbackData.Equals("tomorrowPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectTomorrowPeriodHandler>();
        }

        if (callbackData.Equals("afterTomorrowPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectAfterTomorrowPeriodHandler>();
        }

        if (callbackData.Equals("weekPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectWeekPeriodHandler>();
        }

        if (callbackData.Equals("monthPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectMonthPeriodHandler>();
        }
        
        if (callbackData.Equals("userPeriodButton"))
        {
            return _serviceProvider.GetRequiredService<SelectUserPeriodHandler>();
        }
        
        throw new ArgumentException("Unrecognized handler");
    }
}