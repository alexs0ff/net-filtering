using RaCruds.Models.Statements;
using RaCruds.Models;

namespace RaCruds;
public abstract class EntityFilterBase<TOutDto>
    where TOutDto : class
{
    public abstract Task<PagingResult<TOutDto>> FilterAsync(FilterParameters filterParameters, CancellationToken cancellationToken = default);
}
