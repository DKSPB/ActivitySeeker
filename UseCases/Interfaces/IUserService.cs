using ActivitySeeker.UseCases.Models;
using Domain.Entities;

namespace ActivitySeeker.UseCases.Interfaces;

public interface IUserService
{
    /// <summary>
    /// Добавление нового пользователя
    /// </summary>
    /// <param name="user">Объект - пользователь</param>
    Task CreateUser(UserDto user);
    
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
    Task<UserDto?> GetUserById(long id);
}