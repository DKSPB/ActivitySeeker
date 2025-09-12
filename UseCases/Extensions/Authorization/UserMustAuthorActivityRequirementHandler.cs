namespace UseCases.Extensions.Authorization
{
    using Interfaces.Repos;
    using MediatR.Behaviors.Authorization;
    public class UserMustAuthorActivityRequirementHandler : IAuthorizationHandler<UserMustAuthorActivityRequirement>
    {
        private readonly IActivityRepository _repository;

        public UserMustAuthorActivityRequirementHandler(IActivityRepository repository)
        {
            _repository = repository;
        }
        public async Task<AuthorizationResult> Handle(UserMustAuthorActivityRequirement request, CancellationToken cancellationToken)
        {
            var activity = await _repository.FindAsync(request.ActivityId, cancellationToken);

            if (activity == null)
            {
                return AuthorizationResult.Fail("Activity not found");
            }
            
            if (request.IsAdmin)
            { 
                return AuthorizationResult.Succeed(); 
            }

            return activity.UserId == request.UserId
            ? AuthorizationResult.Succeed()
            : AuthorizationResult.Fail("Not authorized");
        }
    }
}
