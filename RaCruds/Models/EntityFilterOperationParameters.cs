namespace RaCruds.Models;

public class EntityFilterOperationParameters
{
    public string DefaultOrderColumn { get; set; }

    public IDictionary<string, string> FieldsMapping { get; } = new Dictionary<string, string>();

    public static EntityFilterOperationParameters Empty { get; } = new EntityFilterOperationParameters();
}
