using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IAdminService
{
    Task RegisterAsync(string userName, string login, string password);

    Task<string> LoginAsync(string userName, string password);
    Task<IEnumerable<Admin>> GetAll();
}