namespace ActivitySeeker.Bll.Interfaces;

public interface IAdminService
{
    Task RegisterAsync(string userName, string login, string password);

    Task<string> LoginAsync(string userName, string password);
}