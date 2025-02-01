using Microsoft.EntityFrameworkCore;

namespace RaCruds;
internal interface IDbContextFactory<TEntity, TOutDto>
    where TEntity : class
    where TOutDto : class
{
    DbContext GetContext();
}
