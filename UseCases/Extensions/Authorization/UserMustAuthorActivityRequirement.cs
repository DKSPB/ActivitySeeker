using MediatR.Behaviors.Authorization;

namespace UseCases.Extensions.Authorization;

public class UserMustAuthorActivityRequirement : IAuthorizationRequirement
{
    public long? UserId { get; set; }
    public bool IsAdmin { get; set; }
    public Guid ActivityId { get; set; }

}