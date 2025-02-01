﻿using System.ComponentModel;
using System.Linq.Expressions;

namespace RaCruds.Extensions;
public static class QueryableExtensions
{
    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortProperty,
        ListSortDirection sortOrder)
    {
        var type = typeof(T);
        var property = TypesExtensions.GetExtendedPropertyType(type, sortProperty);
        var parameter = Expression.Parameter(type, "p");
        var propertyAccess = parameter.CreateNestedPropertyExpression(sortProperty);
        var orderByExp = Expression.Lambda(propertyAccess, parameter);
        var typeArguments = new Type[] { type, property.PropertyType };
        var methodName = sortOrder == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
        var resultExp = Expression.Call(typeof(Queryable), methodName, typeArguments, source.Expression,
            Expression.Quote(orderByExp));

        return source.Provider.CreateQuery<T>(resultExp);
    }
}