using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RaCruds.Models.Statements;

namespace RaCruds.Infrastructure;
public class FilterParametersParser
{
    public FilterParameters ParseModel(IQueryCollection queryParameters)
    {
        var columnNames = queryParameters[FilterColumnName];
        var groups = columnNames.GroupBy(s => s).Select(
            s => new { Name = s.Key, Count = s.Count() });

        var filter = new FilterParameters();

        if (queryParameters.ContainsKey(StatementsParam))
        {
            filter.Statements = ParseJsonStatements(queryParameters[StatementsParam]);
        }

        if (queryParameters.ContainsKey(PageSize))
        {
            if (int.TryParse(queryParameters[PageSize], NumberStyles.None, CultureInfo.InvariantCulture,
                out var res))
            {
                filter.PageSize = res;
            }
        }

        filter.PageSize = ParseInt(queryParameters, PageSize, -1);
        filter.CurrentPage = ParseInt(queryParameters, CurrentPage, -1);

        if (queryParameters.ContainsKey(OrderBy))
        {
            filter.OrderKind = ParseEnum(queryParameters, OrderKind, 1,
                    Models.Statements.OrderKind.Asc)
                .First();
            filter.OrderBy = queryParameters[OrderBy];
        }

        if (filter.Statements == null)
        {
            filter.Statements = [];
        }

        return filter;
    }

    private FilterStatement[]? ParseJsonStatements(StringValues queryParameter)
    {        
        var options = new JsonSerializerOptions();
        options.Converters.Add(new JsonStringEnumConverter());
        options.PropertyNameCaseInsensitive = true;
        var rawValue = queryParameter.ToString();
        return JsonSerializer.Deserialize<FilterStatement[]>(rawValue, options);        
    }

    private int ParseInt(IQueryCollection queryParameters, string parameterName, int defaultValue)
    {
        var result = defaultValue;
        if (queryParameters.ContainsKey(parameterName))
        {
            if (int.TryParse(queryParameters[parameterName], NumberStyles.None, CultureInfo.InvariantCulture,
                out var res))
            {
                result = res;
            }
        }

        return result;
    }

    private List<TEnum> ParseEnum<TEnum>(IQueryCollection queryParameters, string parameterName, int expectedCount,
        TEnum defaultValue)
        where TEnum : Enum
    {
        List<TEnum> result = new List<TEnum>();

        if (queryParameters.ContainsKey(parameterName))
        {
            var comparisonEnumRaw = queryParameters[parameterName];
            for (int i = 0; i < expectedCount; i++)
            {
                if ((comparisonEnumRaw.Count > i) &&
                    Enum.TryParse(typeof(TEnum), comparisonEnumRaw[i], true, out var co))
                {
                    result.Add((TEnum)co);
                }
                else
                {
                    result.Add(defaultValue);
                }
            }


        }
        else
        {
            result.AddRange(Enumerable.Repeat(defaultValue, expectedCount));
        }

        return result;
    }


    private const string FilterColumnName = "filtercolumnname";

    private const string FilterColumnValue = "filtercolumn{0}value";

    private const string FilterParameterOperator = "filtercolumn{0}operator";

    private const string FilterParameterNext = "filtercolumn{0}logic";

    private const string PageSize = "pagesize";

    private const string CurrentPage = "page";

    private const string OrderBy = "OrderBy";

    private const string OrderKind = "OrderKind";

    private const string StatementsParam = "statements";
}
