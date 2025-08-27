using MediatR.Behaviors.Authorization;
using UseCases.Extensions.Authorization;
using UseCases.Interfaces.Auth;

namespace UseCases.Activity.Commands.Update;

public class UpdateActivityCommandAuthorizer
{
    public class GetCourseVideoDetailAuthorizer : AbstractRequestAuthorizer<UpdateActivityCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public GetCourseVideoDetailAuthorizer(ICurrentUserService currentUserService)
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
}