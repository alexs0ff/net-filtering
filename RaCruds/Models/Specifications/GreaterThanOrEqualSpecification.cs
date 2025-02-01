using System.Linq.Expressions;
using RaCruds.Extensions;

namespace RaCruds.Models.Specifications;
internal sealed class GreaterThanOrEqualSpecification<T> : ISpecification<T>
    where T : class
{
    private readonly Type _valueType;

    private readonly object _value;

    private readonly string _propertyName;

    public GreaterThanOrEqualSpecification(Type valueType, object value, string propertyName)
    {
        _valueType = valueType;
        _value = value;
        _propertyName = propertyName;
    }

    public Expression<Func<T, bool>> ToExpression()
    {
        var item = Expression.Parameter(typeof(T), "e");

        var nestedBody = item.CreateNestedPropertyExpression(_propertyName);

        var valueConstant = Expression.Constant(_value, _valueType);

        var equal = Expression.GreaterThanOrEqual(nestedBody, valueConstant);

        var lambda = Expression.Lambda<Func<T, bool>>(equal, item);

        return lambda;
    }
}
