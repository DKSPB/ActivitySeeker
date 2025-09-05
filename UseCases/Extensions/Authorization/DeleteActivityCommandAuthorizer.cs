using UseCases.Interfaces.Auth;
using MediatR.Behaviors.Authorization;
using UseCases.Activity.Commands.Delete;

namespace UseCases.Extensions.Authorization
{
    public class DeleteActivityCommandAuthorizer : AbstractRequestAuthorizer<DeleteActivityCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteActivityCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteActivityCommand request)
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
