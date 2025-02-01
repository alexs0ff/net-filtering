using RaCruds.Config;
using RaCruds.Config.Configurations;
using RaCruds.Extensions;
using ShopApp.Entities;

namespace ShopApp.RaCruds;

public static class Registrations
{
    public static IServiceCollection AddRaCruds(this IServiceCollection serviceCollection)
    {
        var entityTopology = new EntityTopology();
        var configurator = new EntityTopologyConfigurator(entityTopology);
        configurator.AddFilter<Cart, CartDto>()
            .ForDbContext<ShopAppContext>()
            .WithParameters(p =>
            {
                p.IncludeProperties = [nameof(Cart.Order), nameof(Cart.CartItems)];
            })
            .AddCustomSpecification<CartItemNameSpecification>("cartItemName")
            .CompleteFilter()
            .Complete();

        serviceCollection.RegisterEntities(entityTopology);

        return serviceCollection;
    }
}
