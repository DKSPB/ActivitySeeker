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
        userExists.CityId = user.CityId;
        userExists.State = user.State.StateNumber;
        userExists.ChatId = user.ChatId;
        userExists.UserName = user.UserName;
        userExists.ActivityResult = JsonConvert.SerializeObject(user.ActivityResult);
        userExists.ActivityFormat = user.State.ActivityFormat;
        userExists.ActivityTypeId = user.State.ActivityType.Id;
        userExists.SearchFrom = user.State.SearchFrom.GetValueOrDefault();
        userExists.SearchTo = user.State.SearchTo.GetValueOrDefault();

        if (user.Offer is null)
        {
            userExists.Offer = null;
        }
        else if (userExists.Offer is null)
        {
            userExists.Offer = user.Offer.ToActivity();
        }
        else
        {
            userExists.Offer.Id = user.Offer.Id;
            userExists.Offer.LinkOrDescription = user.Offer.LinkOrDescription;
            userExists.Offer.ActivityTypeId = user.Offer.ActivityTypeId;
            userExists.Offer.IsPublished = user.Offer.OfferState;
            userExists.Offer.StartDate = user.Offer.StartDate;
            userExists.Offer.Image = user.Offer.Image;
        }

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
        var user = _context.Users
                .Include(x => x.ActivityType)
                .Include(z => z.Offer)
                .ThenInclude(y => y!.ActivityType)
                .FirstOrDefault(x=>x.Id == id);
        
        return user is null ? null : new UserDto(user);
    }
}