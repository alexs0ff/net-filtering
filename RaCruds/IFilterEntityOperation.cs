using RaCruds.Models;
using RaCruds.Models.Statements;

namespace RaCruds;

public interface IFilterEntityOperation<TOutDto, TFilterableOperationParameters>
    where TOutDto : class
    where TFilterableOperationParameters : IFilterableOperationParameters
{
    Task<PagingResult<TOutDto>> FilterAsync(TFilterableOperationParameters operationParameters, FilterParameters filterParameters, CancellationToken cancellationToken = default);
}
