namespace RaCruds.Models.Statements;
public class FilterStatement
{
    public FilterStatement[] Statements { get; set; }

    public FilterLogicalOperators LogicalOperator { get; set; }

    public string ParameterName { get; set; }

    public string ParameterValue { get; set; }

    public string ComparisonOperator { get; set; }
}
