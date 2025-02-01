using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaCruds;

internal static class PageDataCalculator
{
    public const int DefaultPageSize = 50;

    public const int MaxPageSize = 100;

    public static (int skip, int take) GetPageData(int pageSize, int currentPage, bool useDefaults)
    {
        if (pageSize <= 0 || currentPage <= 0)
        {
            if (useDefaults)
            {
                return (0, DefaultPageSize);
            }

            return (-1, -1);
        }

        if (pageSize > MaxPageSize)
        {
            pageSize = MaxPageSize;
        }

        var skip = pageSize * (currentPage - 1);

        return (skip, pageSize);
    }
}
