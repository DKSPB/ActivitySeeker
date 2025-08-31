using MediatR.Behaviors.Authorization;
using UseCases.Activity.Commands.Update;
using UseCases.Interfaces.Auth;

namespace UseCases.Extensions.Authorization;

public class UpdateActivityCommandAuthorizer : AbstractRequestAuthorizer<UpdateActivityCommand>
{
    private readonly ICurrentUserService _currentUserService;

    public UpdateActivityCommandAuthorizer(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override void BuildPolicy(UpdateActivityCommand request)
    {
        UseRequirement(new UserMustAuthorActivityRequirement
        {
            ActivityId = request.Id,
            UserId = _currentUserService.UserId
        });
    }
}