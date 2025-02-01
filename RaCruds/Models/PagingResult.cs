namespace RaCruds.Models;
public class PagingResult<TPagedEntity>
    where TPagedEntity : class
{
    public PagingResult(long count, IEnumerable<TPagedEntity> entities)
    {
        Count = count;
        Entities = entities;
    }

    public long Count { get; }

    public IEnumerable<TPagedEntity> Entities { get; }

    static PagingResult()
    {
        Empty = new PagingResult<TPagedEntity>(0, Enumerable.Empty<TPagedEntity>());
    }

    public static PagingResult<TPagedEntity> Empty { get; }
}
