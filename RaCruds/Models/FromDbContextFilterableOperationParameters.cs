using Microsoft.EntityFrameworkCore;

namespace RaCruds.Models;
public class FromDbContextFilterableOperationParameters<TEntity> : IFilterableOperationParameters
    where TEntity : class
{
    public DbContext DbContext { get; set; }
}
