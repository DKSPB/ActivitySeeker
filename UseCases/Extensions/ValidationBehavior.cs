using FluentValidation;
using MediatR;
using UseCases.Interfaces;

namespace UseCases.Extensions;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequireOwnership<TEntity, object>
{
    private readonly ICurrentUserService _currentUser;
    private readonly IEntityAuthorization _authorization;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(
        ICurrentUserService currentUser,
        IEntityAuthorization authorization,
        IEnumerable<IValidator<TRequest>> validators)
    {
        _currentUser = currentUser;
        _authorization = authorization;
        _validators = validators;
    }
    
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAdmin || _currentUser.UserId is null)
        {
            throw new UnauthorizedAccessException("Требуется аутентификация.");
        }

        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
        }

        await _authorization.EnsureCanModifyAsync (
            request.Id,
            _currentUser.UserId!.Value,
            cancellationToken);

        return await next(cancellationToken);
    }
}