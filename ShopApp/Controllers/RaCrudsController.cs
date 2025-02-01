using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using RaCruds;
using RaCruds.Infrastructure;
using RaCruds.Models;
using RaCruds.Models.Statements;
using ShopApp.RaCruds;

namespace ShopApp.Controllers;

public class RaCrudsController : Controller
{
    [HttpGet("raCarts")]
    [ProducesResponseType(typeof(PagingResult<CartDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetRaCarts([ModelBinder(typeof(FilterParametersModelBinder))] FilterParameters filterParameters, [FromServices] EntityFilterBase<CartDto> entityFetcher, CancellationToken cancellationToken)
    {
        var pagingResult = await entityFetcher.FilterAsync(filterParameters, cancellationToken);

        return Json(pagingResult, new JsonSerializerOptions
        {   
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}
