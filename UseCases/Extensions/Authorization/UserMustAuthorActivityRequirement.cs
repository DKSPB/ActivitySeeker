using DataAccess.Interfaces;
using MediatR.Behaviors.Authorization;
using Microsoft.EntityFrameworkCore;
using UseCases.Common;

namespace UseCases.Extensions.Authorization;

public class UserMustAuthorActivityRequirement : IAuthorizationRequirement
{
    public long? UserId { get; set; }
    
    public bool IsAdmin { get; set; }
    public Guid ActivityId { get; set; }

    class UserMustAuthorActivityRequirementHandler : IAuthorizationHandler<UserMustAuthorActivityRequirement>
    {
        private readonly IDbContext _context;

        public UserMustAuthorActivityRequirementHandler(IDbContext context)
        {
            _context = context;
        }
        
        public async Task<AuthorizationResult> Handle(UserMustAuthorActivityRequirement request, CancellationToken cancellationToken)
        {
            var userCourseSubscription = await _context.Activities
                .FirstOrDefaultAsync(x => x.Id == request.ActivityId
                                          && x.UserId == request.UserId, cancellationToken: cancellationToken);

            return userCourseSubscription != null 
                ? AuthorizationResult.Succeed() 
                : AuthorizationResult.Fail("Нет доступа для модификации объекта");
        }
    }
}