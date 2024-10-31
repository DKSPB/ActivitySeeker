using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

/// <summary>
/// Сервис доступа с сущности City
/// </summary>
public interface ICityService
{
    /// <summary>
    /// Получить список городов, в названиях которых встречается подстрока name
    /// </summary>
    /// <param name="name">Подстрока, котороя содержится в названиях городов</param>
    /// <returns></returns>
    Task<IEnumerable<City>> GetCitiesByName(string name);
}