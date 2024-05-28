using ActivitySeeker.Bll.Models;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll;

public class UserDto
{
    public long Id { get; set; }
    
    public long ChatId { get; set; }

    public string UserName { get; set; } = default!;
    
    public int MessageId { get; set; }

    public LinkedList<ActivityDto> Result { get; set; } = new();

    public ActivityRequest ActivityRequest { get; set; } = new();

    public UserDto(User user)
    {
        Id = user.Id;
        ChatId = user.ChatId;
        UserName = user.UserName;
        MessageId = user.MessageId;
        Result = JsonConvert.DeserializeObject<LinkedList<ActivityDto>>(user.ActivityResult) ??
                 new LinkedList<ActivityDto>();
        
        ActivityRequest = new ActivityRequest
        {
            ActivityTypeId = user.ActivityTypeId,
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
            ActivityTypeId = user.ActivityRequest.ActivityTypeId,
            SearchFrom = user.ActivityRequest.SearchFrom,
            SearchTo = user.ActivityRequest.SearchTo,
            ActivityResult = JsonConvert.SerializeObject(user.Result)
        };
    }
}