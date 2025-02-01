using Microsoft.EntityFrameworkCore;

namespace RaCruds;
internal class DbContextFactory<TEntity, TOutDto> : IDbContextFactory<TEntity, TOutDto>
    where TEntity : class
    where TOutDto : class
{
    private readonly DbContext _dbContext;

    public DbContextFactory(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbContext GetContext()
    {
        return _dbContext;
    }
}
