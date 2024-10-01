using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
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
                    InlineKeyboardButton.WithCallbackData("Тип активности", StatesEnum.ActivityTypeChapter.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Время проведения", StatesEnum.ActivityPeriodChapter.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Найти", StatesEnum.Result.GetDisplayName())
                }
            });
        }

        public static InlineKeyboardMarkup GetOfferKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Добавить ссылку", StatesEnum.OfferActivityLink.GetDisplayName()), 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Добавить описание", StatesEnum.OfferActivityLink.GetDisplayName()), 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", StatesEnum.MainMenu.GetDisplayName())
                }
            });
        }
        
        public static InlineKeyboardMarkup GetPinOfferPictureKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Да", StatesEnum.OfferActivityLink.GetDisplayName()), 
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Нет", StatesEnum.AddOfferDate.GetDisplayName()), 
                }
            });
        }

        public static InlineKeyboardMarkup ConfirmOffer()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Подтвердить мероприятие", StatesEnum.ConfirmOffer.GetDisplayName()), 
                }
            });
        }

        public static InlineKeyboardMarkup GetActivityTypesKeyboard(List<ActivityType> activityTypes) 
        {
            List<List<InlineKeyboardButton>> activityTypeButtons = new();
            
            /*activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Все виды активностей", Guid.Empty.ToString())
            });*/

            foreach (var activityType in activityTypes)
            {
                activityTypeButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(activityType.TypeName, activityType.Id.ToString())
                });
            }

            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData("Назад", StatesEnum.MainMenu.GetDisplayName())
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
                    InlineKeyboardButton.WithCallbackData("Сегодня", StatesEnum.TodayPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Завтра", StatesEnum.TomorrowPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Послезавтра", StatesEnum.AfterTomorrowPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За неделю", StatesEnum.WeekPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("За месяц", StatesEnum.MonthPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Свой период", StatesEnum.UserPeriod.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", StatesEnum.MainMenu.GetDisplayName())
                },
            });
        }

        public static InlineKeyboardMarkup GetActivityPaginationKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Назад", StatesEnum.PreviousActivity.GetDisplayName()),
                    InlineKeyboardButton.WithCallbackData("Вперёд", StatesEnum.NextActivity.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню", StatesEnum.MainMenu.GetDisplayName())
                }
            });
        }

        public static InlineKeyboardMarkup GetToMainMenuKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Меню", StatesEnum.MainMenu.GetDisplayName())
                }
            });
        }
    }
}
