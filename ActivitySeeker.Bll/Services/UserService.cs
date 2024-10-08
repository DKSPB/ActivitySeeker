using ActivitySeeker.Bll.Interfaces;
using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Services;

public class UserService: IUserService
{
    private readonly ActivitySeekerContext _context;
    
    public UserService(ActivitySeekerContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public void UpdateUser(UserDto user)
    {
        var userExists = _context.Users.First(x => x.Id == user.Id);
        
        userExists.MessageId = user.State.MessageId;
        userExists.State = user.State.StateNumber;
        userExists.ChatId = user.ChatId;
        userExists.UserName = user.UserName;
        userExists.ActivityResult = JsonConvert.SerializeObject(user.ActivityResult);
        userExists.ActivityTypeId = user.State.ActivityType.Id;
        userExists.SearchFrom = user.State.SearchFrom.GetValueOrDefault();
        userExists.SearchTo = user.State.SearchTo.GetValueOrDefault();

        _context.Users.Update(userExists);
        _context.SaveChanges();

    }
    
    /// <inheritdoc />
    public void CreateUser(UserDto user)
    {
        var userEntity = UserDto.ToUser(user);
        _context.Users.Add(userEntity);
        _context.SaveChanges();
    }
    
    /// <inheritdoc />
    public UserDto? GetUserById(long id)
    {
        var user = _context.Users.Include(x => x.ActivityType).FirstOrDefault(x=>x.Id == id);
        
        return user is null ? null : new UserDto(user);
    }
}