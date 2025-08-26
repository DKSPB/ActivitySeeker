using DataAccess.Interfaces;
using MediatR.Behaviors.Authorization;
using UseCases.Common;
using UseCases.Interfaces.Auth;

namespace UseCases.Activity.Commands.Update;

public class MustBeAuthorRequirement : IAuthorizationRequirement
{
    public Guid Id { get; set; }
    
    public bool IsAdmin { get; set; }
    public long? UserId { get; set; }
}

public class MustBeAuthorRequirementHandler : IAuthorizationHandler<MustBeAuthorRequirement>
{
    private readonly IDbContext _context;

    public MustBeAuthorRequirementHandler(IDbContext context)
    {
        _context = context;
    }

    public async Task<AuthorizationResult> Handle(MustBeAuthorRequirement requirement, CancellationToken cancellationToken = default)
    {
        var activity = await _context.Activities.FindAsync(new object[] { requirement.Id }, cancellationToken) 
                       ??  throw new ObjectNotFoundException(nameof(Domain.Entities.Activity), requirement.Id);

        if (requirement.IsAdmin)
        {
            return AuthorizationResult.Succeed();
        }
        
        return activity.UserId == requirement.UserId
            ? AuthorizationResult.Succeed()
            : AuthorizationResult.Fail("Пользователь не авторизован");
    }
    
    public class ActivityAuthorizer : AbstractRequestAuthorizer<UpdateActivityCommand>
    {
        private readonly ICurrentUserService _currentUser;

        public ActivityAuthorizer(ICurrentUserService currentUser)
        {
            _currentUser = currentUser;
        }
        
        public override void BuildPolicy(UpdateActivityCommand request)
        {
            UseRequirement(new MustBeAuthorRequirement
            {
                Id = request.Id,
                IsAdmin = _currentUser.IsAdmin,
                UserId = _currentUser.UserId
            });
        }
    }
}