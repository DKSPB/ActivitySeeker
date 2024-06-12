using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using System.Globalization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot.Handlers
{
    public class SelectUserPeriodHandler
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
            if (update.Message != null)
            {
                var chat = update.Message.Chat;
                try
                {
                    if (!string.IsNullOrEmpty(update.Message.Text))
                    {
                        var user = update.Message.From;

                        if (user is null)
                        {
                            throw new NullReferenceException("User in null");
                        }


                        var message = await _botClient.SendTextMessageAsync(
                            chat.Id,
                            text: "Ввведите дату, с которой хотите",
                            replyMarkup: Keyboards.GetMainMenuKeyboard(),
                            cancellationToken: cancellationToken);

                        //currentUser.MessageId = message.MessageId;
                        //_userService.CreateOrUpdateUser(currentUser);
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
