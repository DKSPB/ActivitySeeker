using System.Text;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

public class UserDto
{
    public long Id { get; set; }
    
    public long ChatId { get; set; }

    public string UserName { get; set; } = default!;

    public LinkedList<ActivityDto> ActivityResult { get; set; } = default!;

    public State State { get; set; } = new();

    public UserDto()
    {
        
    }
    public UserDto(User user)
    {
        Id = user.Id;
        ChatId = user.ChatId;
        UserName = user.UserName;
        ActivityResult = JsonConvert.DeserializeObject<LinkedList<ActivityDto>>(user.ActivityResult) ??
                         new LinkedList<ActivityDto>();
        
        State = new State
        {
            //ActivityTypeId = user.ActivityTypeId,
            //ActivityType = user.ActivityType?.TypeName?? "Все виды активности",
            SearchFrom = user.SearchFrom,
            SearchTo = user.SearchTo,
            MessageId = user.MessageId,
            StateNumber = user.State
        };

        State.ActivityType = user.ActivityTypeId == null
            ? new ActivityTypeDto()
            : new ActivityTypeDto
            {
                Id = user.ActivityTypeId,
                TypeName = user.ActivityType.TypeName
            };
    }
    
    public static User ToUser(UserDto user)
    {
        return new User
        {
            Id = user.Id,
            ChatId = user.ChatId,
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