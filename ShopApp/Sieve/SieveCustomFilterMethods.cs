using ShopApp.Entities;
using Sieve.Services;

namespace ShopApp.Sieve;

public class SieveCustomFilterMethods : ISieveCustomFilterMethods
{
    public IQueryable<Cart> CartItemName(IQueryable<Cart> source, string op, string[] values)
    {
        var result = source.Where(p => p.CartItems.Any(i => i.Name.Contains(values.Single())));

        return result;
    }
}