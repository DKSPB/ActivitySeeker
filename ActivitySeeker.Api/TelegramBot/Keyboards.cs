using ActivitySeeker.Domain.Entities;
using System.Diagnostics.Metrics;
using Newtonsoft.Json;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot
{
    public static class Keyboards
    {
        public static InlineKeyboardMarkup GetMainMenuKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Тип активности", "SelectActivityTypeHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Время проведения", "SelectActivityPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Найти", "SearchResultHandler")
                }
            });
        }

        public static InlineKeyboardMarkup GetActivityTypesKeyboard(List<ActivityType> activityTypes) 
        {
            List<List<InlineKeyboardButton>> activityTypeButtons = new();
            
            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Все виды активностей", "activityType/")
            });

            foreach (var activityType in activityTypes)
            {
                activityTypeButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(activityType.TypeName, string.Concat("activityType/", activityType.Id))
                });
            }

            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад", "MainMenuHandler")
            });

            InlineKeyboardMarkup countersButtons = new (activityTypeButtons);


            return countersButtons;
        }

        public static InlineKeyboardMarkup GetPeriodActivityKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Сегодня", "SelectTodayPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Завтра", "SelectTomorrowPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Послезавтра", "SelectAfterTomorrowPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За неделю", "SelectWeekPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За месяц", "SelectMonthPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Свой период", "SelectUserPeriodHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "MainMenuHandler")
                },
            });
        }

        public static InlineKeyboardMarkup GetActivityPaginationKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "PreviousHandler"),
                    InlineKeyboardButton.WithCallbackData("Вперёд", "NextHandler")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню", "MainMenuHandler")
                }
            });
        }
    }
}
