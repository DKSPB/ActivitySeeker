using MediatR.Behaviors.Authorization;
using MediatR.Behaviors.Authorization.Interfaces;

namespace UseCases.Extensions.Authorization;

public class DefaultAuthorizer : IUnauthorizedResultHandler
{
    public Task<TResponse?> Invoke<TResponse>(AuthorizationResult result)
    {
        return Task.FromResult(default(TResponse));
    }
}