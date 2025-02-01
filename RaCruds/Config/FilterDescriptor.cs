using RaCruds.Models;

namespace RaCruds.Config;
internal sealed class FilterDescriptor
{
    public Type? EntityType { get; set; }

    public Type? FilterableEntityType { get; set; }

    public Type? DbContextType { get; set; }

    public EntityFilterOperationParameters EntityFilterOperationParameters { get; set; }

    public Func<IServiceProvider, object> DbContextFactory { get; set; }
    
    public IDictionary<string, Type> CustomSpecifications { get; } = new Dictionary<string, Type>();
}
