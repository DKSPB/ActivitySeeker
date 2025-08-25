namespace UseCases.Interfaces;

public interface IEntityAuthorization<TEntity, TId>
{
    Task EnsureCanModifyAsync(TId entityId, long userId, CancellationToken cancellationToken);
}