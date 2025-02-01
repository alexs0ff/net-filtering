using Microsoft.Extensions.DependencyInjection;
using RaCruds.Config;
using RaCruds.DirectDbContext;

namespace RaCruds.Extensions;
public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterEntities(this IServiceCollection serviceCollection, EntityTopology entityTopology)
    {
        serviceCollection.AddSingleton<IFilterDescriptorContainer, FilterDescriptorContainer>();

        foreach (var descriptor in entityTopology.FilterDescriptors)
        {
            serviceCollection.AddSingleton(descriptor);
            var entityFilterBaseType = typeof(EntityFilterBase<>).MakeGenericType(descriptor.FilterableEntityType);
            Type entityFilterImplementationType = null;

            if (descriptor.EntityFilterOperationParameters is EntityContextFilterOperationParameters)
            {
                entityFilterImplementationType =
                    typeof(EntityFilterFromDbContext<,>).MakeGenericType(descriptor.FilterableEntityType,
                        descriptor.EntityType);
            }

            if (entityFilterImplementationType != null)
            {
                serviceCollection.AddScoped(entityFilterBaseType, entityFilterImplementationType);
            }

            var filterFromDbContextEntityOperationType =
                typeof(IFilterFromDbContextEntityOperation<,>).MakeGenericType(descriptor.FilterableEntityType,
                    descriptor.EntityType);

            var filterFromDbContextEntityOperationImplementationType = typeof(EntityContextFilterOperation<,>).MakeGenericType(descriptor.FilterableEntityType,
                descriptor.EntityType);

            serviceCollection.AddScoped(filterFromDbContextEntityOperationType,
                filterFromDbContextEntityOperationImplementationType);

            var dbContextBaseType = typeof(IDbContextFactory<,>).MakeGenericType(descriptor.EntityType, descriptor.FilterableEntityType);

            serviceCollection.AddScoped(dbContextBaseType, descriptor.DbContextFactory);
        }

        return serviceCollection;
    }
}
