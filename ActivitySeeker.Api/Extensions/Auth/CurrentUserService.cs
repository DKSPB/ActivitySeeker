using System.Security.Claims;
using UseCases.Interfaces.Auth;

namespace ActivitySeeker.Api.Extensions.Auth;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public long? UserId
    {
        get
        {
            var userIdStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return long.TryParse(userIdStr, out var userId) ? userId : null;
        }
    }

    public bool IsAdmin => true;
}