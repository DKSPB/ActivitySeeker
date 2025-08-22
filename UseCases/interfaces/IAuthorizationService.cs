namespace UseCases.Interfaces;

public interface IEntityAuthorization<TId>
{
    Task EnsureCanModifyAsync(TId entityId, long userId, CancellationToken cancellationToken);
}