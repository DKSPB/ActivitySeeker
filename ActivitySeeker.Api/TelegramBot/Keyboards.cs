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

            InlineKeyboardMarkup countersButtons = new InlineKeyboardMarkup(activityTypeButtons);


            return countersButtons;
        }
    }
    
    /// <summary>
    /// Содержит информацию о том, выбран тип активности или нет
    /// </summary>
    /*public class ActivityTypeInfo
    {
        private const string CheckMark = "\u2705 ";

        private string _buttonText = default!;
        
        public ActivityTypeInfo(string buttonId, string buttonType, string buttonText, bool selected)
        {
            ButtonId = buttonId;
            ButtonType = buttonType;
            Selected = selected;
            ButtonText = buttonText;
        }

        /// <summary>
        /// Идентификатор кнопки
        /// </summary>
        public string ButtonId { get; set; }
        
        /// <summary>
        /// текст кнопки
        /// </summary>
        public string ButtonText
        {
            get => _buttonText;
            private set => _buttonText = Selected is false ? value : string.Concat(CheckMark, value);
        }

        /// <summary>
        /// Тип кнопки
        /// </summary>
        public string ButtonType { get; set; }

        /// <summary>
        /// Выбрана кнопка или нет
        /// </summary>
        public bool Selected { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string GetData()
        {
            return JsonConvert.SerializeObject(new { ButtonId });
        }
    }*/
}
