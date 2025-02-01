namespace RaCruds.Models.Statements;
public class FilterCompositeStatement : FilterStatementBase
{
    public FilterStatement[] Statements { get; set; }
}
