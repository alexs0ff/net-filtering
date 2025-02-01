using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using ShopApp.Entities;

namespace ShopApp.Controllers;

public class OdataCartsController : ODataController
{
    private readonly ShopAppContext _appContext;

    public OdataCartsController(ShopAppContext appContext)
    {
        _appContext = appContext;
    }

    [EnableQuery]
    public ActionResult<IQueryable<Cart>> Get()
    {
        return Ok(_appContext.Carts);
    }
}