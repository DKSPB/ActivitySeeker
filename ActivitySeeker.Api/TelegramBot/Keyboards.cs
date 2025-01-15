using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
using Telegram.Bot.Types.ReplyMarkups;

namespace ActivitySeeker.Api.TelegramBot
{
    public static class Keyboards
    {
        public static InlineKeyboardMarkup GetEmptyKeyboard()
        {
            return InlineKeyboardMarkup.Empty();
        }

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
                    InlineKeyboardButton.WithCallbackData("Формат проведения", StatesEnum.SelectActivityFormat.GetDisplayName())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Найти", StatesEnum.Result.GetDisplayName())
                }
            });
        }

        public static InlineKeyboardMarkup ConfirmOffer()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("К началу", StatesEnum.Offer.GetDisplayName()),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Предложить активность", StatesEnum.ConfirmOffer.GetDisplayName()), 
                }
            });
        }

        public static InlineKeyboardMarkup GetActivityTypesKeyboard(List<ActivityTypeDto> activityTypes, string backButtonValue,  string backButtonText = "Назад") 
        {
            List<List<InlineKeyboardButton>> activityTypeButtons = new();
            foreach (var activityType in activityTypes)
            {
                activityTypeButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(activityType.TypeName, activityType.Id.ToString())
                });
            }

            activityTypeButtons.Add(new List<InlineKeyboardButton>
            {
                InlineKeyboardButton.WithCallbackData(backButtonText, backButtonValue)
            });

            InlineKeyboardMarkup countersButtons = new (activityTypeButtons);


            return countersButtons;
        }

        public static InlineKeyboardMarkup GetActivityFormatsKeyboard(bool withAny)
        {
            var buttons = new List<InlineKeyboardButton[]>
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Онлайн", "online"),
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Офлайн", "offline"),
                }
            };
            
            if (withAny)
            {
                buttons.Add( new []
                {
                    InlineKeyboardButton.WithCallbackData("Любой формат", "any"),
                });
                buttons.Add(new[]
                {
                    InlineKeyboardButton.WithCallbackData("Назад", StatesEnum.MainMenu.GetDisplayName())
                });
            }
            
            return new InlineKeyboardMarkup(buttons);
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

        public static InlineKeyboardMarkup GetDefaultSettingsKeyboard(int mskId, int spbId, bool withSkip)
        {
            var buttons = new List<InlineKeyboardButton[]>
            {
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Москва", mskId.ToString())
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Санкт-Петербург", spbId.ToString())
                }
            };

            if (withSkip)
            {
                const int skip = -1;
                buttons.Add( new[]
                {
                    InlineKeyboardButton.WithCallbackData("Пропустить", skip.ToString())
                });
            }
            return new InlineKeyboardMarkup(buttons);
        }

        public static InlineKeyboardMarkup GetCityKeyboard(IEnumerable<City> cities)
        {
            List<List<InlineKeyboardButton>> activityTypeButtons = new();
            
            foreach (var city in cities)
            {
                activityTypeButtons.Add(new List<InlineKeyboardButton>
                {
                    InlineKeyboardButton.WithCallbackData(city.Name, city.Id.ToString())
                });
            }

            InlineKeyboardMarkup countersButtons = new (activityTypeButtons);


            return countersButtons;
        }

        public static InlineKeyboardMarkup GetOfferMenuKeyboard()
        {
            return new InlineKeyboardMarkup(new List<InlineKeyboardButton[]>
            {
                new []
                {
                    InlineKeyboardButton.WithCallbackData("Тип активности", "")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Время проведения", "")
                },
                new[]
                {
                    InlineKeyboardButton.WithCallbackData("Формат проведения", "")
                }
            });
        }
    }
}
