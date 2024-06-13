using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    public class SelectUserPeriodHandler: IHandler
    {
        private readonly ITelegramBotClient _botClient;
        private readonly IUserService _userService;

        public SelectUserPeriodHandler(ITelegramBotClient botClient, IUserService userService)
        {
            _botClient = botClient;
            _userService = userService;
        }

        public async Task HandleAsync(Update update, CancellationToken cancellationToken)
        {
            var callbackQuery = update.CallbackQuery;
            
            await _botClient.AnswerCallbackQueryAsync(callbackQuery.Id, cancellationToken: cancellationToken);
            
            var currentUser = GetCurrentUser(callbackQuery);
            
            var message = await _botClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                text: $"Введите дату, с которой хотите искать активностив форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.MessageId = message.MessageId;
            _userService.CreateOrUpdateUser(currentUser);
        }
        
        private UserDto GetCurrentUser(CallbackQuery callbackQuery)
        {
            var currentUserId = callbackQuery.From.Id;
        
            return _userService.GetUserById(currentUserId);
        }

        class UserDateParse
        {
            DateTime _userDate;

            bool ParseResult { get; set; }

            void ParseUserDate (string date)
            {
                var format = "dd.MM.yyyy HH:mm";
                bool ParseResult = DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _userDate);
            }
        }
    }
}
