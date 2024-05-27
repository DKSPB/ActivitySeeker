using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using ActivitySeeker.Api.TelegramBot.Handlers;
namespace ActivitySeeker.Api.TelegramBot;

public class MessageHandler
{
    private readonly StartHandler _startHandler;

    public MessageHandler(StartHandler startHandler)
    {
        _startHandler = startHandler;
    }

    public async Task Handle(Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
            {
                await _startHandler.HandleAsync(update, cancellationToken);
                return;
            }
            
            case UpdateType.CallbackQuery:
            {
                var callbackQuery = update.CallbackQuery;

                if (callbackQuery is null)
                {
                    throw new NullReferenceException("Callback query is null");
                }

                //try
                //{
                    /*if (callbackQuery.Data is null)
                    {
                        throw new NullReferenceException("Object Data is null");
                    }
                    
                    if(callbackQuery.Data.Equals("mainMenu"))
                    {
                        AbstractHandler mainMenuHandler = new MainMenuHandler();
                        await mainMenuHandler.HandleAsync(callbackQuery, cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("selectActivityTypeButton"))
                    {
                        AbstractHandler selectActivityTypeHandler = new SelectActivityTypeHandler();

                        await selectActivityTypeHandler.HandleAsync(callbackQuery, cancellationToken);
                    }

                    if (callbackQuery.Data.Contains("activityType"))
                    {
                        AbstractHandler listOfActivitiesHandler = new ListOfActivitiesHandler();
                        await listOfActivitiesHandler.HandleAsync(callbackQuery, cancellationToken);
                    }*/

                    /*if (callbackQuery.Data.Equals("searchActivityButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        var activities = _activityService.GetActivities(new ActivityRequest());
                        var currentActivity = activities.First();
                        currentActivity.Selected = true;

                        await _botClient.EditMessageReplyMarkupAsync(
                            chatId: callbackQuery.Message.Chat.Id,
                            messageId: currentUser.MessageId,
                            replyMarkup: InlineKeyboardMarkup.Empty(),
                            cancellationToken);
                        
                        var message = await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: currentActivity.Activity.Name,
                            replyMarkup: Keyboards.GetActivityPaginationKeyboard(),
                            cancellationToken: cancellationToken);
                        
                        currentUser.MessageId = message.MessageId;
                        currentUser.ActivityResult = JsonConvert.SerializeObject(activities);
                        _userService.CreateOrUpdateUser(currentUser);
                    }

                    if (callbackQuery.Data.Equals("next"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        var activities = JsonConvert.DeserializeObject<LinkedList<ActivityDto>>(currentUser.ActivityResult);
                        var selectedActivity = activities.FirstOrDefault(x => x.Selected);

                        ActivityDto currentActivity = null;
                        if (selectedActivity is null)
                        {
                            activities.First().Selected = true;
                            currentActivity = activities.First();
                        }
                        else
                        {
                            var nextNode = activities.Find(selectedActivity).Next;
                            if (nextNode is not null)
                            {
                                nextNode.Previous.Value.Selected = false;
                                nextNode.Value.Selected = true;
                                currentActivity = nextNode.Value;
                            }
                            
                        }
                        currentUser.ActivityResult = JsonConvert.SerializeObject(activities);

                        await _botClient.DeleteMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            currentUser.MessageId,
                            cancellationToken);
                        
                        var message = await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: currentActivity is null ? "Это все события :(" : currentActivity.Activity.Name,
                            replyMarkup: Keyboards.GetActivityPaginationKeyboard(),
                            cancellationToken: cancellationToken);
                        
                        currentUser.MessageId = message.MessageId;

                        _userService.CreateOrUpdateUser(currentUser);
                    }
                    
                    if (callbackQuery.Data.Equals("back"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        var activities = JsonConvert.DeserializeObject<LinkedList<ActivityDto>>(currentUser.ActivityResult);
                        var selectedActivity = activities.FirstOrDefault(x => x.Selected);

                        ActivityDto currentActivity = null;
                        if (selectedActivity is null)
                        {
                            activities.First().Selected = true;
                            currentActivity = activities.First();
                        }
                        else
                        {
                            var previousNode = activities.Find(selectedActivity).Previous;
                            if (previousNode is not null)
                            {
                                previousNode.Next.Value.Selected = false;
                                previousNode.Value.Selected = true;
                                currentActivity = previousNode.Value;
                            }
                            
                        }
                        currentUser.ActivityResult = JsonConvert.SerializeObject(activities);

                        await _botClient.DeleteMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            currentUser.MessageId,
                            cancellationToken);
                        
                        var message = await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: currentActivity is null ? "Это все события :(" : currentActivity.Activity.Name,
                            replyMarkup: Keyboards.GetActivityPaginationKeyboard(),
                            cancellationToken: cancellationToken);
                        
                        currentUser.MessageId = message.MessageId;

                        _userService.CreateOrUpdateUser(currentUser);
                    }*/
                    
                    /*if (callbackQuery.Data.Equals("mainMenu"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(nn
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        await _queryContext.DoLoadCounters(CurrentState);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: "Список сайтов:",
                            replyMarkup: KeyboardGenerator.GetCountersKeyboard(CurrentState.Counters),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Contains("counter"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        var currentCounterId = callbackQuery.Data.Split('/')[1];
                        CurrentState.SetCurrentCounter(currentCounterId);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("reportFilters"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: $"Здесь пока ничего нет",
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("reportPeriod"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: "Период отчёта:",
                            replyMarkup: KeyboardGenerator.ListOfPeriodsKeyboard(CurrentState.CurrentCounter.Id),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("goals"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        await _queryContext.DoLoadGoals(CurrentState);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: "Цели:",
                            replyMarkup: KeyboardGenerator.GetGoalsKeyboard(CurrentState.CurrentCounter),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Contains("selectedGoal"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(
                            callbackQuery.Id, cancellationToken: cancellationToken);

                        var currentGoalId = callbackQuery.Data.Split('/')[1];
                        CurrentState.SetCurrentGoal(currentGoalId);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("todayPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now;
                        CurrentState.Date2 = DateTime.Now;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("yesterdayPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now.AddDays(-1);
                        CurrentState.Date2 = CurrentState.Date1;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("weekPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now.AddDays(-7);
                        CurrentState.Date2 = DateTime.Now;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("monthPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now.AddMonths(-1).AddDays(1);
                        CurrentState.Date2 = DateTime.Now;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("quarterPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now.AddMonths(-4).AddDays(1);
                        CurrentState.Date2 = DateTime.Now;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("yearPeriodButton"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        CurrentState.Date1 = DateTime.Now.AddYears(-1).AddDays(1);
                        CurrentState.Date2 = DateTime.Now;

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, CurrentState.WriteReportSettings(),
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    if (callbackQuery.Data.Equals("createReport"))
                    {
                        await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id,
                            cancellationToken: cancellationToken);

                        if (string.IsNullOrEmpty(CurrentState.CurrentGoal.Id))
                        {
                            await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: $"Выберите цель",
                                replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                                cancellationToken: cancellationToken);
                        }

                        var metrics = $"ym:s:users,ym:s:goal{CurrentState.CurrentGoal.Id}visits";
                        CurrentState.Metrics = metrics;
                        var goalInfo = await _queryContext.GetGoalsData(CurrentState);

                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id,
                            text: $"Целевые визиты: {goalInfo}",
                            replyMarkup: KeyboardGenerator.ReportSettingsKeyboard(),
                            cancellationToken: cancellationToken);
                    }

                    await _queryContext.SaveState(CurrentState);*/

                    return;
                //}
                /*catch (Exception e)
                {
                    await _botClient.SendTextMessageAsync(
                        callbackQuery.Message.Chat.Id, text: e.Message, cancellationToken: cancellationToken);
                }*/

                return;
            }
        }
    }
}