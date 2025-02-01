namespace RaCruds.Models.Statements;
public class  FilterParameters
{
    public FilterParameters()
    {
        Statements = new FilterStatement[0];
    }

    public FilterStatement[] Statements { get; set; }

    public int PageSize { get; set; }

    public int CurrentPage { get; set; }

    public string OrderBy { get; set; }

    public OrderKind OrderKind { get; set; }
}
