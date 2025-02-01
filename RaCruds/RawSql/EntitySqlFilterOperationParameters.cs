using RaCruds.Models;

namespace RaCruds.RawSql;
public class EntitySqlFilterOperationParameters : EntityFilterOperationParameters
{
    public string Sql { get; set; }

    public string AliasName { get; set; }

    public SqlDialect Dialect { get; set; } = SqlDialect.TSql;

    /// <summary>
    /// If true - filter by TEntity type, otherwise - TOutDto.
    /// </summary>
    public bool FilterByEntityType { get; set; }
}
