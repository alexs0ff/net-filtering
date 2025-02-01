using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopApp.Entities;
using ShopApp.Sieve;
using Sieve.Models;
using Sieve.Services;

namespace ShopApp.Controllers;

public class SieveCartsController : Controller
{
    private readonly ISieveProcessor _processor;

    private readonly ShopAppContext _appContext;

    public SieveCartsController(ISieveProcessor processor, ShopAppContext appContext)
    {
        _processor = processor;
        _appContext = appContext;
    }

    [HttpGet("sieveCarts")]
    public async Task<JsonResult> GetSieveCarts(SieveModel sieveModel)
    {
        var result = _appContext.Carts
            .Include(i=>i.Order)
            .Include(i=>i.CartItems)
            .AsNoTracking();

        //TotalCount: https://github.com/Biarity/Sieve/issues/34

        var countQuery = _processor.Apply(sieveModel, result, applyPagination: false);

        var count = await countQuery.CountAsync();

        result = _processor.Apply(sieveModel, result);

        return Json(new SieveResult<Cart>()
        {
            Items = result.ToArray(),
            Count = count
        },new JsonSerializerOptions
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles,
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase)
            },
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
    }
}