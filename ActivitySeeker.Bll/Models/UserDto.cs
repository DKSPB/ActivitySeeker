using System.Text;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

public class UserDto
{
    public long Id { get; set; }
    
    public long ChatId { get; set; }

    public string UserName { get; set; } = default!;
    
    public int MessageId { get; set; }
    
    public StatesEnum State { get; set; }

    public LinkedList<ActivityDto> ActivityResult { get; set; } = default!;

    public ActivityRequest ActivityRequest { get; set; } = new();

    public UserDto()
    {
        
    }
    public UserDto(User user)
    {
        Id = user.Id;
        ChatId = user.ChatId;
        UserName = user.UserName;
        MessageId = user.MessageId;
        State = user.State;
        ActivityResult = JsonConvert.DeserializeObject<LinkedList<ActivityDto>>(user.ActivityResult) ??
                 new LinkedList<ActivityDto>();
        
        ActivityRequest = new ActivityRequest
        {
            ActivityTypeId = user.ActivityTypeId,
            ActivityType = user.ActivityType?.TypeName?? "Все виды активности",
            SearchFrom = user.SearchFrom,
            SearchTo = user.SearchTo
        };
    }
    
    public static User ToUser(UserDto user)
    {
        return new User
        {
            Id = user.Id,
            ChatId = user.ChatId,
            UserName = user.UserName,
            MessageId = user.MessageId,
            State = user.State,
            ActivityTypeId = user.ActivityRequest.ActivityTypeId,
            SearchFrom = user.ActivityRequest.SearchFrom,
            SearchTo = user.ActivityRequest.SearchTo,
            ActivityResult = JsonConvert.SerializeObject(user.ActivityResult)
        };
    }
}