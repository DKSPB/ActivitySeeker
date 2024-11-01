using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

/// <summary>
/// Сервис доступа с сущности City
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Получение списка городов, в названиях которых встречается подстрока name
    /// </summary>
    /// <param name="name">Подстрока, котороя содержится в названиях городов</param>
    /// <returns></returns>
    Task<IEnumerable<City>> GetCitiesByName(string name);

    /// <summary>
    /// Получение города по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор города</param>
    /// <returns></returns>
    Task<City?> GetById(int id);
}