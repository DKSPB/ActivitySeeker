using UseCases.Interfaces.Common;

namespace UseCases.Common;

public class PagingSpecification<T> : ISpecification<T>
{
    private readonly int _limit;

    private readonly int _offset;

    public PagingSpecification(int limit, int offset)
    {
        _limit = limit;
        _offset = offset;
    }
    
    public IQueryable<T> Apply(IQueryable<T> query)
    {
        return query.Skip(Math.Max(0, (_offset - 1) * _limit)).Take(_limit);
    }
}