using RaCruds.Models;

namespace RaCruds.DirectDbContext;
public class EntityContextFilterOperationParameters : EntityFilterOperationParameters
{
    public string[] IncludeProperties { get; set; }

    public bool DirectProject { get; set; } = true;
}
