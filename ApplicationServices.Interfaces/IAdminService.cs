using Domain.Entities;

namespace ApplicationServices.Interfaces;

public interface IAdminService
{
    Task RegisterAsync(string login, string password);

    Task<string> LoginAsync(string userName, string password);
    Task<IEnumerable<Admin>> GetAll();
}