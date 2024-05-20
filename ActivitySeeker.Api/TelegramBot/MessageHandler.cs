using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using User = ActivitySeeker.Domain.Entities.User;

namespace ActivitySeeker.Api.TelegramBot;

public class MessageHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly IActivityService _activityService;
    private readonly ActivitySeekerContext _context;

    public MessageHandler(ITelegramBotClient botClient, IUserService userService, IActivityService activityService, ActivitySeekerContext context)
    {
        _context = context;
        _userService = userService;
        _activityService = activityService;
        _botClient = botClient;
    }

    public async Task Handle(Update update, CancellationToken cancellationToken)
    {
        switch (update.Type)
        {
            case UpdateType.Message:
                {
                    if (update.Message != null)
                    {
                        var chat = update.Message.Chat;
                        try
                        {
                            if (update.Message.Text is not null && update.Message.Text.Equals("/start"))
                            {
                                var user = update.Message.From;
                                if (user is null)
                                {
                                    throw new NullReferenceException("User in null");
                                }

                                var message = await _botClient.SendTextMessageAsync(
                                    chat.Id,
                                    text: "Выбери тип активности и время проведения",
                                    replyMarkup: Keyboards.GetMainMenuKeyboard(),
                                    cancellationToken: cancellationToken);

                                var currentUser = new User
                                {
                                    Id = user.Id,
                                    UserName = user.Username ?? "",
                                    ChatId = chat.Id,
                                    MessageId = message.MessageId,
                                    ActivityTypeId = Guid.Empty,
                                    ActivityResult = JsonConvert.SerializeObject(new LinkedList<Activity>())
                                   
                                };
                                
                                _userService.CreateOrUpdateUser(currentUser);
                            }
                            else
                            {
                                await _botClient.SendTextMessageAsync(
                                    chat.Id, "Нераспознанная команда", cancellationToken: cancellationToken);
                            }
                        }
                        catch (Exception e)
                        {
                            await _botClient.SendTextMessageAsync(
                                chat.Id, e.Message, cancellationToken: cancellationToken);
                        }
                    }
                    else
                    {
                        throw new NullReferenceException("Object Message is null");
                    }

                    return;
                }

            case UpdateType.CallbackQuery:
                {
                    var callbackQuery = update.CallbackQuery;

                    if (callbackQuery is null)
                    {
                        throw new NullReferenceException("Callback query is null");
                    }

                    var currentUserId = callbackQuery.From.Id;
                    var currentUser = _userService.GetUserById(currentUserId);
                    
                    try
                    {
                        if(callbackQuery.Data.Equals("mainMenu"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            await _botClient.EditMessageReplyMarkupAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                messageId: currentUser.MessageId,
                                replyMarkup: InlineKeyboardMarkup.Empty(),
                                cancellationToken
                            );
                            
                            var message = await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: "Выбери тип активности и время проведения",
                                replyMarkup: Keyboards.GetMainMenuKeyboard(),
                                cancellationToken: cancellationToken);

                            currentUser.MessageId = message.MessageId;
                            _userService.CreateOrUpdateUser(currentUser);
                        }

                        if (callbackQuery.Data.Equals("selectActivityTypeButton"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            await _botClient.EditMessageReplyMarkupAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                messageId: currentUser.MessageId,
                                replyMarkup: InlineKeyboardMarkup.Empty(),
                                cancellationToken
                            );

                            var keyboard = Keyboards.GetActivityTypesKeyboard(_context.ActivityTypes.ToList());
                            
                            var message = await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: "Типы активностей:",
                                replyMarkup: keyboard,
                                cancellationToken: cancellationToken);
                            
                            currentUser.MessageId = message.MessageId;
                            _userService.CreateOrUpdateUser(currentUser);
                        }

                        if (callbackQuery.Data.Contains("activityType"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            var selectedActivityTypeId = callbackQuery.Data.Split('/')[1];
                            var activityTypes = _context.ActivityTypes.ToList();

                            await _botClient.EditMessageReplyMarkupAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                messageId: currentUser.MessageId,
                                replyMarkup: InlineKeyboardMarkup.Empty(),
                                cancellationToken);

                            var message = await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: "Выбери тип активности и время проведения",
                                replyMarkup: Keyboards.GetMainMenuKeyboard(),
                                cancellationToken: cancellationToken);

                            currentUser.MessageId = message.MessageId;
                            currentUser.ActivityTypeId = Guid.Parse(selectedActivityTypeId);
                            _userService.CreateOrUpdateUser(currentUser);
                        }

                        if (callbackQuery.Data.Equals("searchActivityButton"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            var activities = _activityService.GetActivities(new ActivityRequest());
                            var currentActivity = activities.First();

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

                            ActivityDto currentActivity;
                            if (selectedActivity is null)
                            {
                                activities.First().Selected = true;
                                currentActivity = activities.First();
                            }
                            else
                            {
                                var nextNode = activities.Find(selectedActivity).Next;
                                nextNode.Previous.Value.Selected = false;
                                nextNode.Value.Selected = true;
                                currentActivity = nextNode.Value;
                            }
                            currentUser.ActivityResult = JsonConvert.SerializeObject(activities);
                            
                            var message = await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: currentActivity.Activity.Name,
                                replyMarkup: Keyboards.GetActivityPaginationKeyboard(),
                                cancellationToken: cancellationToken);
                            
                            currentUser.MessageId = message.MessageId;

                            _userService.CreateOrUpdateUser(currentUser);
                        }
                        
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
                    }
                    catch (Exception e)
                    {
                        await _botClient.SendTextMessageAsync(
                            callbackQuery.Message.Chat.Id, text: e.Message, cancellationToken: cancellationToken);
                    }

                    return;
                }
        }
    }

    /*private void InitializeUserDefaultData(Domain.Entities.User user)
    {
        var activityTypesInfo = _context.ActivityTypes.Select(x => 
            new ActivityTypeInfo(x.Id.ToString(), "activityType", x.TypeName, false)).ToList();

        user.State = JsonConvert.SerializeObject(activityTypesInfo);
    }*/
}