using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    Task CreateUserAsync(UserDto user);
    
    /// <summary>
    /// Обновление данных существующего пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    Task UpdateUser(UserDto user);

    /// <summary>
    /// Получение пользователя по идентификаторуs
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Объект - пользователь или null</returns>
    Task<UserDto?> GetUserByIdAsync(long id);
}