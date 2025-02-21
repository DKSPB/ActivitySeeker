using ActivitySeeker.Domain.Entities;

namespace ActivitySeeker.Api.TelegramBot;

[AttributeUsage(AttributeTargets.Class)]
public class HandlerStateAttribute: Attribute
{
    public string /*StatesEnum*/ HandlerState { get; }
    
    public HandlerStateAttribute(/*StatesEnum*/ string state)
    {
        HandlerState = state;
    }
}