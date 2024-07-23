using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerStateAttribute: Attribute
{
    public StatesEnum HandlerState { get; }
    public HandlerStateAttribute(StatesEnum state)
    {
        HandlerState = state;
    }
}