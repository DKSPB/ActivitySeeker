using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityService
{
    /// <summary>
    /// Получение связанного списка, содержащего активности, найденные по запросу пользователя
    /// </summary>
    /// <param name="requestParams">Объект, содержащий запрос пользователя</param>
    /// <returns>Двусвязный список активностей</returns>
    LinkedList<ActivityTelegramDto> GetActivitiesLinkedList(State requestParams);

    /// <summary>
    /// Получение списка активностей
    /// </summary>
    /// <param name="requestParams">Объект, содержащий запрос пользователя</param>
    /// <returns>Список активностей</returns>
    IQueryable<Activity>? GetActivities(ActivityRequest requestParams);

    /// <summary>
    /// Получение списка всех типов активностей
    /// </summary>
    /// <returns></returns>
    List<ActivityType> GetActivityTypes();

    /// <summary>
    /// Получение типа активностей по идентификатору
    /// </summary>
    /// <param name="activityId">Идентификатор типа активности</param>
    /// <returns></returns>
    ActivityType GetActivityType(Guid activityId);

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
    Task<ActivityDto> GetActivityAsync(Guid activityId);

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
    Task DeleteActivity(List<ActivityDto> activitiesForRemove);

    /// <summary>
    /// Публикация активностей из списка
    /// </summary>
    /// <param name="activityIds">Список идентификаторов активностей</param>
    /// <returns></returns>
    Task PublishActivities(List<Guid> activityIds);
}