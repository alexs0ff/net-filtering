using System.Collections.Concurrent;

namespace RaCruds.Config;

internal class FilterDescriptorContainer : IFilterDescriptorContainer
{

    private readonly ConcurrentDictionary<Type, FilterDescriptor> _filterDescriptors;

    public FilterDescriptorContainer(IEnumerable<FilterDescriptor> descriptors)
    {
        var dic = descriptors.ToDictionary(k => k.FilterableEntityType);
        _filterDescriptors = new ConcurrentDictionary<Type, FilterDescriptor>(dic);
    }

    public FilterDescriptor GetFor<TOutDto>()
    {
        var type = typeof(TOutDto);
        if (!_filterDescriptors.ContainsKey(type))
        {
            throw new EntityTypeFilterNotRegisteredException(type);
        }

        return _filterDescriptors[type];
    }
}
