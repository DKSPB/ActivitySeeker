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
    /// Получение всех городов
    /// </summary>
    /// <returns></returns>
    Task<List<City>> GetCities();

    /// <summary>
    /// Получение города по его идентификатору
    /// </summary>
    /// <param name="id">Идентификатор города</param>
    /// <returns></returns>
    Task<City?> GetById(int id);

    /// <summary>
    /// Загрузка изображения города по его названию
    /// </summary>
    /// <param name="cityId">Номер города</param>
    /// <param name="path">путь к файлу</param>
    /// <param name="image">Объект-поток изображения</param>
    /// <returns></returns>
    Task UploadImage(int cityId, string path, Stream image);
}