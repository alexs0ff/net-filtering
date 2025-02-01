using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;
using ShopApp.Entities;

namespace ShopApp.OData;

public static class Registrations
{
    public static void RegisterOData(this IMvcBuilder mvcBuilder)
    {
        var modelBuilder = new ODataConventionModelBuilder();
        modelBuilder.EnableLowerCamelCase();
        modelBuilder.EnableLowerCamelCaseForPropertiesAndEnums();
        modelBuilder.EnumType<OrderStatus>();
        modelBuilder.EntitySet<CartItem>("CartItems");
        modelBuilder.EntitySet<Order>("Orders");
        modelBuilder.EntitySet<Cart>("OdataCarts");
        mvcBuilder.AddOData(options =>
            options.EnableQueryFeatures(null).AddRouteComponents(
                routePrefix: "odata",
                model: modelBuilder.GetEdmModel()));
    }
}