using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Interfaces;

public interface IUserService
{
    void CreateOrUpdateUser(UserDto user);

    UserDto GetUserById(long id);
}