namespace ActivitySeeker.Bll.Interfaces;

public interface IAdminService
{
    Task RegisterAsync(string userName, string password);

    Task<string> LoginAsync(string userName, string password);
}