using UseCases.Interfaces.Auth;
using MediatR.Behaviors.Authorization;
using UseCases.Activity.Commands.Publish;


namespace UseCases.Extensions.Authorization
{
    public class PublishActivityCommandAuthorizer : AbstractRequestAuthorizer<PublishActivityCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public PublishActivityCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(PublishActivityCommand request)
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
