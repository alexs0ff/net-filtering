namespace RaCruds.Config.Configurations;
public class EntityTopologyConfigurator
{
    private readonly EntityTopology _entityTopology;

    public EntityTopologyConfigurator(EntityTopology entityTopology)
    {
        _entityTopology = entityTopology;
    }

    public EntityContextFilterConfigurator<TEntity, TOutDto> AddFilter<TEntity, TOutDto>()
        where TEntity : class
        where TOutDto : class
    {
        return new EntityContextFilterConfigurator<TEntity, TOutDto>(this);
    }

    internal EntityTopologyConfigurator AddFilter(FilterDescriptor filterDescriptor)
    {
        _entityTopology.FilterDescriptors.Add(filterDescriptor);
        return this;
    }

    public EntityTopology Complete()
    {
        return _entityTopology;
    }
}
