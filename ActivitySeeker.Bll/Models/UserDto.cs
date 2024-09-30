using System.Text;
using ActivitySeeker.Domain.Entities;
using Newtonsoft.Json;

namespace ActivitySeeker.Bll.Models;

public class UserDto
{
    public long Id { get; set; }
    
    public long ChatId { get; set; }

    public string UserName { get; set; } = default!;

    public LinkedList<ActivityTelegramDto> ActivityResult { get; set; } = default!;

    public State State { get; set; } = new();

    public UserDto()
    {
        
    }
    public UserDto(User user)
    {
        Id = user.Id;
        ChatId = user.ChatId;
        UserName = user.UserName;
        ActivityResult = JsonConvert.DeserializeObject<LinkedList<ActivityTelegramDto>>(user.ActivityResult) ??
                         new LinkedList<ActivityTelegramDto>();
        
        State = new State
        {
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
            UserName = user.UserName,
            MessageId = user.State.MessageId,
            State = user.State.StateNumber,
            SearchFrom = user.State.SearchFrom.GetValueOrDefault(),
            SearchTo = user.State.SearchTo.GetValueOrDefault(),
            ActivityResult = JsonConvert.SerializeObject(user.ActivityResult)
        };
    }
}