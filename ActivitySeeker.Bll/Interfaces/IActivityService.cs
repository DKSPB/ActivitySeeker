using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityService
{
    /// <summary>
    /// Получение связанного списка, содержащего активности, найденные по запросу пользователя
    /// </summary>
    /// <param name="currentUser">Объект, содержащий запрос пользователя</param>
    /// <returns>Двусвязный список активностей</returns>
    LinkedList<ActivityTelegramDto> GetActivitiesLinkedList(UserDto currentUser);

    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="requestParams">Объект, содержащий запрос пользователя</param>
    /// <returns>Список активностей</returns>
    IQueryable<Activity>? GetActivities(ActivityRequest requestParams);

    /// <summary>
    /// Получение активностей по типу
    /// </summary>
    /// <param name="activityTypeId">Идентификатор типа активностей</param>
    /// <returns></returns>
    Task<List<ActivityDto>> GetActivitiesByType(Guid activityTypeId);

    /// <summary>
    /// Получение информации об активности по идентфиикатору
    /// </summary>
    /// <param name="activityId">Идентификатор активности</param>
    /// <returns></returns>
    Task<ActivityDto?> GetActivityAsync(Guid activityId);

    /// <summary>
    /// Получение изображения от активности
    /// </summary>
    /// <param name="activityId">Идентификатор активности</param>
    /// <returns>Массив байтов изображения</returns>
    Task<byte[]?> GetImage(Guid activityId);
    
    /// <summary>
    /// Добавление новой активности
    /// </summary>
    /// <param name="newActivity"></param>
    Task CreateActivity(ActivityDto newActivity);

    /// <summary>
    /// Обновление существующей активности
    /// </summary>
    Task UpdateActivity(ActivityDto activity);
    
    /// <summary>
    /// Удаление активностей
    /// </summary>
    /// <param name="activitiesForRemove">Список объектов для удаления</param>
    Task DeleteActivity(List<Guid> activitiesForRemove);

    /// <summary>
    /// Публикация активности
    /// </summary>
    /// <param name="activity">Публикуемая активность</param>
    /// <param name="tgMessageId">Номер сообщения в канале</param>
    /// <returns></returns>
    Task PublishActivity(ActivityDto activity, int tgMessageId);

    /// <summary>
    /// Снятие активности с публикации
    /// </summary>
    /// <param name="activity">Активность, которую нужно снять с публикации</param>
    /// <returns></returns>
    Task WithdrawFromPublication(ActivityDto activity);

    Task RemoveOldActivities();
}