using System.Globalization;

namespace RaCruds.Extensions;
public static class FilterTypeCorrector
{
    private static readonly string[] DateFormats = new[]
        {@"yyyy-MM-dd\THH:mm:ss.fff\Z", "yyyy-MM-dd HH:mm:ss.fff", "yyyyMMdd"};

    private static readonly string[] DateTimeOffsetFormats = new[] { "yyyy-MM-dd HH:mm:ss.fff zzz" };

    private static readonly string[] TimeFormats = new[] { @"hh\:mm\:ss\.fff" };

    public static object? ChangeType<TEntity>(string parameterName, string parameterValue)
    {
        var property = typeof(TEntity).GetExtendedPropertyType(parameterName);

        if (property == null)
        {
            return null;
        }

        var targetType = property.PropertyType;

        if (targetType.IsEnum)
        {
            targetType = Enum.GetUnderlyingType(targetType);
        }

        object value = parameterValue;

        if (targetType == typeof(int))
        {
            if (!int.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
            {
                return null;
            }

            value = res;
        }
        else if (targetType == typeof(long))
        {
            if (!long.TryParse(parameterValue, NumberStyles.None, CultureInfo.InvariantCulture, out var res))
            {
                return null;
            }

            value = res;
        }
        else if (targetType == typeof(Guid))
        {
            if (!Guid.TryParse(parameterValue, out var res))
            {
                return null;
            }

            value = res;
        }
        else if (targetType == typeof(decimal))
        {
            if (!decimal.TryParse(parameterValue, out var res))
            {
                return null;
            }

            value = res;
        }
        else if (targetType == typeof(DateTime))
        {
            foreach (var dateFormat in DateFormats)
            {
                if (DateTime.TryParseExact(parameterValue, dateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var d))
                {
                    return d;
                }
            }

            return null;
        }
        else if (targetType == typeof(DateTimeOffset))
        {
            foreach (var dateFormat in DateTimeOffsetFormats)
            {
                if (DateTimeOffset.TryParseExact(parameterValue, dateFormat, CultureInfo.InvariantCulture,
                    DateTimeStyles.None, out var dateTimeOffset))
                {
                    return dateTimeOffset;
                }
            }

            return null;
        }
        else if (targetType == typeof(TimeSpan))
        {
            if (TimeSpan.TryParseExact(parameterValue, TimeFormats, CultureInfo.InvariantCulture, out var time))
            {
                return time;
            }

            return null;
        }

        if (value != null && property.PropertyType.IsEnum)
        {
            value = Enum.ToObject(property.PropertyType, value);
        }

        return value;
    }

    public static object GetDefaultValue<TEntity>(string parameterName)
    {
        var property = typeof(TEntity).GetExtendedPropertyType(parameterName);

        if (property == null)
        {
            return null;
        }

        object value = null;
        var targetType = property.PropertyType;

        if (targetType == typeof(int))
        {
            value = default(int);
        }
        else if (targetType == typeof(long))
        {
            value = default(long);
        }
        else if (targetType == typeof(Guid))
        {
            value = default(Guid);
        }
        else if (targetType == typeof(DateTime))
        {
            value = default(DateTime);
        }
        else if (targetType == typeof(DateTimeOffset))
        {
            value = default(DateTimeOffset);
        }

        return value;
    }
}
