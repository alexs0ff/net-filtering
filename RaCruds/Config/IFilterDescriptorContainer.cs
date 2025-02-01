namespace RaCruds.Config;
internal interface IFilterDescriptorContainer
{
    FilterDescriptor GetFor<TEntity>();
}
