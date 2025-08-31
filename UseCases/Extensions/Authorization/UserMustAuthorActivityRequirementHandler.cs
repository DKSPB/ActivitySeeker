using DataAccess.Interfaces;
using MediatR.Behaviors.Authorization;

namespace UseCases.Extensions.Authorization
{
    public class UserMustAuthorActivityRequirementHandler : IAuthorizationHandler<UserMustAuthorActivityRequirement>
    {
        private readonly IDbContext _context;

        public UserMustAuthorActivityRequirementHandler(IDbContext context)
        {
            _context = context;
        }

        public async Task<AuthorizationResult> Handle(UserMustAuthorActivityRequirement request, CancellationToken cancellationToken)
        {
            var activity = await _context.Activities.FindAsync(new object[] { request.ActivityId }, cancellationToken);

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
