namespace RaCruds.Config;

public class EntityTypeFilterNotRegisteredException : Exception
{
    public Type EntityType { get; }

    public EntityTypeFilterNotRegisteredException(Type entityType)
    {
        EntityType = entityType;
    }
}
