using System.Text;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

public class UserDto
{
    public long Id { get; set; }
    
    public long ChatId { get; set; }
    
    public int? CityId { get; set; }

    public string UserName { get; set; } = default!;

    public LinkedList<ActivityTelegramDto> ActivityResult { get; set; } = default!;

    public State State { get; set; } = new();

    public ActivityDto? Offer { get; set; }
    
    public Guid? OfferId { get; set; }

    public UserDto()
    {
        
    }
    public UserDto(User user)
    {
        Id = user.Id;
        ChatId = user.ChatId;
        CityId = user.CityId;
        UserName = user.UserName;
        Offer = user.Offer is null ? null : new ActivityDto(user.Offer);
        ActivityResult = JsonConvert.DeserializeObject<LinkedList<ActivityTelegramDto>>(user.ActivityResult) ??
                         new LinkedList<ActivityTelegramDto>();
        OfferId = user.OfferId;
        
        State = new State
        {
            ActivityType = user.ActivityType == null ? new ActivityTypeDto() : new ActivityTypeDto(user.ActivityType),
            SearchFrom = user.SearchFrom,
            SearchTo = user.SearchTo,
            MessageId = user.MessageId,
            StateNumber = user.State,
        };
    }
    
    public static User ToUser(UserDto user)
    {
        return new User
        {
            Id = user.Id,
            ChatId = user.ChatId,
            CityId = user.CityId,
            UserName = user.UserName,
            MessageId = user.State.MessageId,
            State = user.State.StateNumber,
            ActivityTypeId = user.State.ActivityType.Id,
            SearchFrom = user.State.SearchFrom.GetValueOrDefault(),
            SearchTo = user.State.SearchTo.GetValueOrDefault(),
            ActivityResult = JsonConvert.SerializeObject(user.ActivityResult)
        };
    }
}