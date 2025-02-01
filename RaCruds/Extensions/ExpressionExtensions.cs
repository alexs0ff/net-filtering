using System.Linq.Expressions;

namespace RaCruds.Extensions;

public static class ExpressionExtensions
{
    public static Expression CreateNestedPropertyExpression(this ParameterExpression param, string propertyName)
    {
        Expression body = param;
        foreach (var member in propertyName.Split('.'))
        {
            body = Expression.PropertyOrField(body, member);
        }
        return body;
    }
}
