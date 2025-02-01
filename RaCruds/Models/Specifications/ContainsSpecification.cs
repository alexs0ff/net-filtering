using System.Linq.Expressions;
using RaCruds.Extensions;

namespace RaCruds.Models.Specifications;
internal sealed class ContainsSpecification<T> : ISpecification<T>
    where T : class
{
    private readonly string _value;

    private readonly string _propertyName;

    public ContainsSpecification(string value, string propertyName)
    {
        _value = value;
        _propertyName = propertyName;
    }

    public Expression<Func<T, bool>> ToExpression()
    {
        var methodInfo = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });

        var item = Expression.Parameter(typeof(T), "e");

        var nestedBody = item.CreateNestedPropertyExpression(_propertyName);

        var valueConstant = Expression.Constant(_value);

        var body = Expression.Call(nestedBody, methodInfo, valueConstant);

        var lambda = Expression.Lambda<Func<T, bool>>(body, item);

        return lambda;
    }
}
