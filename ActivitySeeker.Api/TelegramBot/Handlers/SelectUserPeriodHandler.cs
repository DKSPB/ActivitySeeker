using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using System.Globalization;
using ActivitySeeker.Domain.Entities;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
            
            await _botClient.EditMessageReplyMarkupAsync(
                chatId: callbackQuery.Message.Chat.Id,
                messageId: currentUser.MessageId,
                replyMarkup: InlineKeyboardMarkup.Empty(),
                cancellationToken
            );
            
            var message = await _botClient.SendTextMessageAsync(
                callbackQuery.Message.Chat.Id,
                text: $"Введите дату, с которой хотите искать активностив форматах:" +
                      $"\n(дд.мм.гггг) или (дд.мм.гггг чч.мм)" +
                      $"\nпример:{DateTime.Now:dd.MM.yyyy} или {DateTime.Now:dd.MM.yyyy HH:mm}",
                cancellationToken: cancellationToken);
            
            currentUser.MessageId = message.MessageId;
            currentUser.State = StatesEnum.PeriodFromDate;
            _userService.CreateOrUpdateUser(currentUser);
        }
        
        private UserDto GetCurrentUser(CallbackQuery callbackQuery)
        {
            var currentUserId = callbackQuery.From.Id;
        
            return _userService.GetUserById(currentUserId);
        }
    }
}
