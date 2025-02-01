using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RaCruds.DirectDbContext;
using RaCruds.Models.Specifications;

namespace RaCruds.Config.Configurations;

public class EntityContextFilterConfigurator<TEntity, TOutDto>
    where TEntity : class
    where TOutDto : class
{
    private readonly EntityTopologyConfigurator _entityTopologyConfigurator;

    private readonly FilterDescriptor _filterDescriptor;

    public EntityContextFilterConfigurator(EntityTopologyConfigurator entityTopologyConfigurator)
    {
        _entityTopologyConfigurator = entityTopologyConfigurator;
        _filterDescriptor = new FilterDescriptor
        {
            EntityType = typeof(TEntity),
            FilterableEntityType = typeof(TOutDto),
            EntityFilterOperationParameters = new EntityContextFilterOperationParameters()
        };
    }

    public EntityContextFilterConfigurator<TEntity, TOutDto> ForDbContext<TDbContext>()
        where TDbContext : DbContext
    {
        _filterDescriptor.DbContextType = typeof(TDbContext);
        _filterDescriptor.DbContextFactory = (sc) =>
            new DbContextFactory<TEntity, TOutDto>(sc.GetRequiredService<TDbContext>());
        return this;
    }

    public EntityContextFilterConfigurator<TEntity, TOutDto> WithParameters(Action<EntityContextFilterOperationParameters> config)
    {
        var parameters = new EntityContextFilterOperationParameters();
        config(parameters);
        _filterDescriptor.EntityFilterOperationParameters = parameters;
        return this;
    }

    public EntityContextFilterConfigurator<TEntity, TOutDto> AddCustomSpecification<TSpecification>(string name)
    where TSpecification : ISpecification<TEntity>
    {
        _filterDescriptor.CustomSpecifications.Add(name, typeof(TSpecification));
        return this;
    }

    public EntityTopologyConfigurator CompleteFilter()
    {
        if (_filterDescriptor.DbContextType == null)
        {
            throw new ArgumentNullException(nameof(_filterDescriptor.DbContextType));
        }
        _entityTopologyConfigurator.AddFilter(_filterDescriptor);

        return _entityTopologyConfigurator;
    }
}
