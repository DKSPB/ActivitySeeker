using MediatR.Behaviors.Authorization;
using UseCases.Activity.Commands.UnPublish;
using UseCases.Interfaces.Auth;

namespace UseCases.Extensions.Authorization
{
    public class UnPublishActivityCommandAuthorizer : AbstractRequestAuthorizer<UnPublishActivityCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        public UnPublishActivityCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(UnPublishActivityCommand request)
        {
            UseRequirement(new UserMustAuthorActivityRequirement
            {
                ActivityId = request.Id,
                IsAdmin = _currentUserService.IsAdmin,
                UserId = _currentUserService.UserId
            });
        }
    }
}
