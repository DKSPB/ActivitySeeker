using ActivitySeeker.Domain.Entities;
using System.Diagnostics.Metrics;
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
                    InlineKeyboardButton.WithCallbackData("Найти", "searcActivityhButton")
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
                    InlineKeyboardButton.WithCallbackData(
                        string.Concat("\u2705 ", activityType.TypeValue), 
                        string.Concat("activityType/" + activityType.Id)) 
                });
            }

            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад", "mainMenu")
            });

            InlineKeyboardMarkup countersButtons = new InlineKeyboardMarkup(activityTypeButtons);


            return countersButtons;
        }
    }
}
