using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Services;

public class UserService: IUserService
{
    private readonly ActivitySeekerContext _context;
    
    public UserService(ActivitySeekerContext context)
    {
        _context = context;
    }
    
    public void CreateOrUpdateUser(UserDto user)
    {
        var userExists = _context.Users.Find(user.Id);

        if (userExists is null)
        {
            _context.Users.Add(UserDto.ToUser(user));
        }
        else
        {
            userExists.MessageId = user.MessageId;
            userExists.ChatId = user.ChatId;
            userExists.UserName = user.UserName;
            userExists.ActivityResult = JsonConvert.SerializeObject(user.ActivityResult);
            userExists.ActivityTypeId = user.ActivityRequest.ActivityTypeId;
            userExists.SearchFrom = user.ActivityRequest.SearchFrom;
            userExists.SearchTo = user.ActivityRequest.SearchTo;

            _context.Users.Update(userExists);
        }
        _context.SaveChanges();
    }

    public UserDto GetUserById(long id)
    {
        var user = _context.Users.Find(id);
        return user is null
            ? throw new NullReferenceException($"Пользователь с таким идентификатором {id} не найден")
            : new UserDto(user);
    }
}