using Domain.Entities;

namespace ApplicationServices.Interfaces;

public interface IActivityTypeService
{
    /// <summary>
    /// Получение списка типов активностей
    /// </summary>
    /// <returns></returns>
    IQueryable<ActivityType> GetAll();
    
    /// <summary>
    /// Создание нового типа активностей
    /// </summary>
    /// <param name="activityType">Объект-тип активностей</param>
    /// <param name="cancellationToken">Объект-токен прерывания</param>
    /// <returns></returns>
    Task Create(ActivityType activityType, CancellationToken cancellationToken = default);
    
    Task Update(ActivityType activityType, CancellationToken cancellationToken = default);
    
    Task Delete(List<Guid> activityTypeIds, CancellationToken cancellationToken = default);
    
}