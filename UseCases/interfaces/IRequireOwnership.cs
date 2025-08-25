namespace UseCases.Interfaces;

public interface IRequireOwnership<TEntity, TId>
{
    TId Id { get; }
}