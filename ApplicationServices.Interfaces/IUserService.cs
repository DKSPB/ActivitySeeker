using Domain.Entities;

namespace ApplicationServices.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    Task CreateUser(User user);
    
    /// <summary>
    /// Обновление данных существующего пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    Task UpdateUser(User user);

    /// <summary>
    /// Получение пользователя по идентификаторуs
    /// </summary>
    /// <param name="id">Идентификатор пользователя</param>
    /// <returns>Объект - пользователь или null</returns>
    Task<User?> GetUserById(long id);
}