using Domain.Entities;

namespace ApplicationServices.Interfaces;

public interface IAdminService
{
    //Task RegisterAsync(string login, string password);

    //Task<string> LoginAsync(string userName, string password);
    /// <summary>
    /// Получение списка администраторов
    /// </summary>
    /// <returns>Список администраторов</returns>
    Task<IEnumerable<Admin>> GetAll();

    /// <summary>
    /// Получение администратора по логину
    /// </summary>
    /// <param name="login">Логин администратора</param>
    /// <returns>Администратор</returns>
    Task<Admin> GetByLogin(string login);
}