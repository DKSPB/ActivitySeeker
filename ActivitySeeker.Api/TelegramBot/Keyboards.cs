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
                    InlineKeyboardButton.WithCallbackData("Тип активности", "selectActivityTypeButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Время проведения", "activityStartPeriodButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Найти", "searchActivityButton")
                }
            });
        }

        public static InlineKeyboardMarkup GetActivityTypesKeyboard(List<ActivityType> activityTypes) 
        {
            List<List<InlineKeyboardButton>> activityTypeButtons = new();

            foreach (var activityType in activityTypes)
            {
                activityTypeButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(activityType.TypeName, string.Concat("activityType/", activityType.Id))
                });
            }

            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад", "mainMenu")
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
                    InlineKeyboardButton.WithCallbackData("Сегодня", "todayPeriodButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Завтра", "tomorrowPeriodButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Послезавтра", "afterTomorrowPeriodButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За неделю", "weekPeriodButton")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За месяц", "monthPeriodButton")
                }
            });
        }

        public static InlineKeyboardMarkup GetActivityPaginationKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", "back"),
                    InlineKeyboardButton.WithCallbackData("Вперёд", "next")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню", "mainMenu")
                }
            });
        }
    }
}
