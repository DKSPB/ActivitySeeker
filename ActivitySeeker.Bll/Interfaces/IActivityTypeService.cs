using ActivitySeeker.Bll.Models;

namespace ActivitySeeker.Bll.Interfaces;

public interface IActivityTypeService
{
    /// <summary>
    /// Получение списка типов активностей
    /// </summary>
    /// <returns></returns>
    Task<List<ActivityTypeDto>> GetAll();

    /// <summary>
    /// Получение типа активностей по модификатору
    /// </summary>
    /// <param name="id">Идентификатор типа активности</param>
    /// <returns></returns>
    Task<ActivityTypeDto> GetById(Guid id);

    /// <summary>
    /// Создание новой активности
    /// </summary>
    /// <param name="activityType">Объект - тип активности</param>
    /// <returns></returns>
    Task Create(ActivityTypeDto activityType);

    /// <summary>
    /// Изменение типа активности
    /// </summary>
    /// <param name="activityType">Объект - тип активности</param>
    /// <returns></returns>
    Task Update(ActivityTypeDto activityType);

    /// <summary>
    /// Удаление типов активностей по идентификаторам
    /// </summary>
    /// <param name="activityTypeIds">Список идентификаторов типов активностей</param>
    /// <returns></returns>
    Task Delete(List<Guid> activityTypeIds);
}