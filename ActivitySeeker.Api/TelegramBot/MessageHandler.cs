using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ActivitySeeker.Api.TelegramBot;

public class MessageHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly IUserService _userService;
    private readonly ActivitySeekerContext _context;

    public MessageHandler(ITelegramBotClient botClient, IUserService userService, ActivitySeekerContext context)
    {
        _context = context;
        _userService = userService;
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
                                _userService.CreateOrUpdateUser(new Domain.Entities.User
                                {
                                    Id = user.Id,
                                    UserName = user.Username?? "",
                                    ChatId = chat.Id,
                                    MessageId = update.Message.MessageId,
                                });

                                await _botClient.SendTextMessageAsync(
                                    chat.Id,
                                    text: "Выбери тип активности и время проведения",
                                    replyMarkup: Keyboards.GetMainMenuKeyboard(),
                                    cancellationToken: cancellationToken);
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

                    try
                    {
                        if(callbackQuery.Data.Equals("mainMenu"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            //await _queryContext.DoLoadCounters(CurrentState);

                            await _botClient.EditMessageReplyMarkupAsync(
                                chatId: callbackQuery.Message.Chat.Id,
                                messageId: 0,
                                replyMarkup: null,
                                cancellationToken
                            );
                            
                            await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: "Выбери тип активности и время проведения",//CurrentState.WriteReportSettings(),
                                replyMarkup: Keyboards.GetMainMenuKeyboard(),
                                cancellationToken: cancellationToken);
                        }

                        if (callbackQuery.Data.Equals("selectActivityTypeButton"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            var activityTypes = _context.ActivityTypes.ToList();

                            await _botClient.SendTextMessageAsync(
                                callbackQuery.Message.Chat.Id,
                                text: "Типы активностей:",
                                replyMarkup: Keyboards.GetActivityTypesKeyboard(activityTypes),
                                cancellationToken: cancellationToken);
                        }

                        if (callbackQuery.Data.Contains("activityType"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
                                callbackQuery.Id, cancellationToken: cancellationToken);

                            var selectedButton = callbackQuery.Data;//.Split('/')[1];
                            var activityTypes = _context.ActivityTypes.ToList();
                            //CurrentState.SetCurrentCounter(currentCounterId);

                            await _botClient.EditMessageTextAsync(
                                callbackQuery.Message.Chat.Id,
                                messageId: callbackQuery.Message.MessageId,
                                "",
                                //text: CurrentState.WriteReportSettings(),
                                replyMarkup: Keyboards.GetActivityTypesKeyboard(activityTypes),
                                cancellationToken: cancellationToken);
                        }

                        /*if (callbackQuery.Data.Equals("mainMenu"))
                        {
                            await _botClient.AnswerCallbackQueryAsync(
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
}