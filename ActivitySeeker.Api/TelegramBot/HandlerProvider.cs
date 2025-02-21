using ActivitySeeker.Api.TelegramBot.Handlers;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Microsoft.OpenApi.Extensions;
using System.Reflection;
using Telegram.Bot.Types;

namespace ActivitySeeker.Api.TelegramBot
{
    /// <summary>
    /// Содержит методы для работы с обработчиками запросов
    /// </summary>
    public static class HandlerProvider
    {
        /// <summary>
        /// Получить все обработчики команд кроме интерфейса и абстрактного класса
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllHandlerTypes()
        {
            var type = typeof(IHandler);

            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)).Where(type => type is { IsClass: true, IsAbstract: false });
        }

        /// <summary>
        /// Найти обработчик команды по состоянию диалога
        /// </summary>
        /// <param name="handlerTypes"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static Type? FindHandlersTypeByState(IEnumerable<Type> handlerTypes, /*StatesEnum*/string state)
        {
            return handlerTypes.FirstOrDefault(x =>
                x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState == state);
        }

        public static Type? FindHandlersTypeByCallbackData(IEnumerable<Type> handlerTypes, string callbackData)
        {
            return handlerTypes.FirstOrDefault(x =>
                x.GetCustomAttribute<HandlerStateAttribute>()?.HandlerState/*.GetDisplayName()*/ == callbackData);
        }

        /// <summary>
        /// Создание экземпляра обработчика по заданному типу
        /// </summary>
        /// <param name="handlerType">Тип обработчика</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static IHandler CreateHandler(IServiceProvider serviceProvider, ILogger logger, Type handlerType)
        {
            var handler = serviceProvider.GetRequiredService(handlerType) as IHandler;

            if (handler is null)
            {
                logger.LogError($"Can not create handler by handler type: {handlerType.FullName}");
                throw new Exception($"Can not create handler by handler type: {handlerType.FullName}");
            }
            return handler;
        }
    }
}
