using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Domain;
using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Bll.Services;

public class UserService: IUserService
{
    private readonly ActivitySeekerContext _context;
    
    public UserService(ActivitySeekerContext context)
    {
        _context = context;
    }
    
    public void CreateOrUpdateUser(User user)
    {
        var userExists = _context.Users.Find(user.Id);

        if (userExists is null)
        {
            _context.Users.Add(user);
        }
        else
        {
            userExists.MessageId = user.MessageId;
            userExists.ChatId = user.ChatId;
            userExists.UserName = user.UserName;
            userExists.State = user.State;

            _context.Users.Update(userExists);
        }
        _context.SaveChanges();
    }

    public User GetUserById(long id)
    {
        var user = _context.Users.Find(id);
        return user ?? throw new NullReferenceException($"Пользователь с таким идентификатором {id} не найден");
    }
}