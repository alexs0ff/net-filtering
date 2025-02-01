using Microsoft.EntityFrameworkCore;
using RaCruds.Models;
using RaCruds.Models.Statements;

namespace RaCruds;
internal class EntityFilterFromDbContext<TOutDto, TEntity> : EntityFilterBase<TOutDto>
    where TOutDto : class
    where TEntity : class
{
    private readonly IFilterFromDbContextEntityOperation<TOutDto, TEntity> _contextEntityOperation;

    private readonly DbContext _dbContext;

    public EntityFilterFromDbContext(IFilterFromDbContextEntityOperation<TOutDto, TEntity> contextEntityOperation, IDbContextFactory<TEntity, TOutDto> dbContextFactory)
    {
        _contextEntityOperation = contextEntityOperation;
        _dbContext = dbContextFactory.GetContext();
    }

    public override async Task<PagingResult<TOutDto>> FilterAsync(FilterParameters filterParameters, CancellationToken cancellationToken = default)
    {
        var parameters = new FromDbContextFilterableOperationParameters<TEntity>();
        parameters.DbContext = _dbContext;
        return await _contextEntityOperation.FilterAsync(parameters, filterParameters, cancellationToken);
    }
}
