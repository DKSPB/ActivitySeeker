using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ActivitySeeker.Api.TelegramBot.Handlers;

public class HandlerFactory
{
    private readonly IServiceProvider _serviceProvider;
    
    public HandlerFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IHandler GetHandler(Update update)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
            {
                if (update.Message != null)
                {
                    if (update.Message.Text is not null && update.Message.Text.Equals("/start"))
                    {
                        return _serviceProvider.GetRequiredService<StartHandler>();
                    }
                }
                else
                {
                    throw new NullReferenceException("Object Message is null");
                }
                
                throw new ArgumentException("Unrecognized handler");
            }
            case UpdateType.CallbackQuery:
            {
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery is null)
                {
                    throw new NullReferenceException("Callback query is null");
                }
                
                if (callbackQuery.Data is null)
                {
                    throw new NullReferenceException("Object Data is null");
                }

                var callbackData = callbackQuery.Data;
                
                if (callbackData.Equals("mainMenu"))
                {
                    return _serviceProvider.GetRequiredService<MainMenuHandler>();
                }
                
                if (callbackData.Equals("selectActivityTypeButton"))
                {
                    return _serviceProvider.GetRequiredService<SelectActivityTypeHandler>();
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
                
                if (callbackData.Contains("activityType"))
                {
                    return _serviceProvider.GetRequiredService<ListOfActivitiesHandler>();
                }
                
                

                throw new ArgumentException("Unrecognized handler");
            }
        }
        throw new ArgumentException("Unrecognized handler");
    }
}