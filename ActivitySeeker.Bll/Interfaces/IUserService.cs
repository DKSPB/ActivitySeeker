using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    void CreateUser(UserDto user);
    
    /// <summary>
    /// Обновление данных существующего пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    void UpdateUser(UserDto user);

    /// <summary>
    /// Получение пользователя по идентификаторуs
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Объект - пользователь или null</returns>
    UserDto? GetUserById(long id);
}