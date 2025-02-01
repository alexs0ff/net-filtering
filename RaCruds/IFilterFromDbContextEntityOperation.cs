using RaCruds.Models;

namespace RaCruds;
internal interface IFilterFromDbContextEntityOperation<TOutDto, TEntity> :
    IFilterEntityOperation<TOutDto, FromDbContextFilterableOperationParameters<TEntity>>
    where TOutDto : class
    where TEntity : class
{

}
