using System.Reflection;

namespace RaCruds.Extensions;
public static class TypesExtensions
{
    public static PropertyInfo? GetPropertyInfoIgnoreCase(this Type type, string name)
    {
        return type.GetProperty(name, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
    }

    public static PropertyInfo? GetExtendedPropertyType(this Type src, string propName)
    {
        if (src == null) throw new ArgumentException("Type cannot be null.", "src");
        if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

        if (propName.Contains("."))//complex type nested
        {
            var temp = propName.Split(['.'], 2);

            return GetExtendedPropertyType(GetExtendedPropertyType(src, temp[0]).PropertyType, temp[1]);
        }
        else
        {
            var prop = src.GetPropertyInfoIgnoreCase(propName);
            return prop;
        }
    }
}
