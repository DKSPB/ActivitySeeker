namespace UseCases.Interfaces;

public interface IRequiredOwnership<TId>
{
    TId EntityId { get; }
}