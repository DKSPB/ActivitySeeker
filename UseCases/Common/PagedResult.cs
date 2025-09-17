namespace UseCases.Common;

public class PagedResult<T>
{
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>()!;
    public int Total { get; set; }
}