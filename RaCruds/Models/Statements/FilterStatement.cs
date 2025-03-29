using System.Text.Json.Serialization;

namespace RaCruds.Models.Statements;
public class FilterStatement
{
    [JsonPropertyName("child")]
    public FilterStatement[] Statements { get; set; }

    [JsonPropertyName("op")]
    public FilterLogicalOperators LogicalOperator { get; set; }

    [JsonPropertyName("name")]
    public string ParameterName { get; set; }

    [JsonPropertyName("value")]
    public string ParameterValue { get; set; }

    [JsonPropertyName("spec")]
    public string ComparisonOperator { get; set; }
}
