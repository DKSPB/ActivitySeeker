using ActivitySeeker.DataAccess.Interfaces.Infrastructure;
using ApplicationServices.Interfaces;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApplicationServices.Implementation;

public class UserService: IUserService
{
    private readonly IDbContext _context;
    
    public UserService(IDbContext context)
    {
        _context = context;
    }
    
    /// <inheritdoc />
    public async Task UpdateUser(User user)
    {
        var userExists = await _context.Users.FirstAsync(x => x.Id == user.Id);
        
        //userExists.MessageId = user.State.MessageId;
        userExists.MessageId = user.MessageId;
        userExists.CityId = user.CityId;
        //userExists.State = user.State.StateNumber;
        userExists.State = user.State;
        userExists.ChatId = user.ChatId;
        userExists.UserName = user.UserName;
        userExists.ActivityResult = JsonConvert.SerializeObject(user.ActivityResult);
        //userExists.ActivityFormat = user.State.ActivityFormat;
        userExists.ActivityFormat = user.ActivityFormat;
        //userExists.ActivityTypeId = user.State.ActivityType.Id;
        userExists.ActivityTypeId = user.ActivityTypeId;
        //userExists.SearchFrom = user.State.SearchFrom.GetValueOrDefault();
        userExists.SearchFrom = user.SearchFrom;
        //userExists.SearchTo = user.State.SearchTo.GetValueOrDefault();
        userExists.SearchTo = user.SearchTo;

        if (user.Offer is null)
        {
            userExists.Offer = null;
        }
        else if (userExists.Offer is null)
        {
            //userExists.Offer = user.Offer.ToActivity();
            userExists.Offer = user.Offer;
        }
        else
        {
            userExists.Offer.Id = user.Offer.Id;
            userExists.Offer.LinkOrDescription = user.Offer.LinkOrDescription;
            userExists.Offer.ActivityTypeId = user.Offer.ActivityTypeId;
            userExists.Offer.IsOnline = user.Offer.IsOnline;
            //userExists.Offer.IsPublished = user.Offer.OfferState;
            userExists.Offer.IsPublished = user.Offer.IsPublished;
            userExists.Offer.StartDate = user.Offer.StartDate;
            userExists.Offer.Image = user.Offer.Image;
            userExists.Offer.CityId = user.Offer.CityId;
        }
        
        await _context.SaveChangesAsync();

    }
    
    /// <inheritdoc />
    public async Task CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    
    /// <inheritdoc />
    public async Task<User?> GetUserById(long id)
    {
        return await _context.Users
                .Include(x => x.ActivityType)
                .Include(z => z.Offer)
                .ThenInclude(y => y!.ActivityType)
                .FirstOrDefaultAsync(x=>x.Id == id);
    }
}