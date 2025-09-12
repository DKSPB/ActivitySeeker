namespace UseCases.Interfaces;

public interface ISpecification<T>
{
    IQueryable<T> Apply(IQueryable<T> query);
}