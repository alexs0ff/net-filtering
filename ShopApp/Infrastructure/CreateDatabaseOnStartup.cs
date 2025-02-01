using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;

namespace ShopApp.Infrastructure;

public static class CreateDatabaseOnStartup
{
    public static void CreateDb(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider
            .GetRequiredService<ShopAppContext>();
        dbContext.Database.Migrate();
    }
}