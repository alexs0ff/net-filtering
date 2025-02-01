using System.Linq.Expressions;
using RaCruds.Models.Specifications;
using ShopApp.Entities;

namespace ShopApp.RaCruds;

public class CartItemNameSpecification : ISpecification<Cart>
{
    private readonly string _value;

    private readonly string _parameter;

    public CartItemNameSpecification(string parameter, string value)
    {
        _value = value;
        _parameter = parameter;
    }

    public Expression<Func<Cart, bool>> ToExpression()
    {
        Expression<Func<Cart, bool>> cartItemNameContains = p => p.CartItems.Any(i => i.Name.Contains(_value));

        return cartItemNameContains;
    }
}
