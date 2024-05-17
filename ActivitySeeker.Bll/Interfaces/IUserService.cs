using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IUserService
{
    void CreateOrUpdateUser(User user);

    User GetUserById(long id);
}