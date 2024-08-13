
using Cwiczenia_6.Models;
using Cwiczenia_6.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia_6.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WarehouseController : ControllerBase
{

    private readonly IWarehouseService _warehouseService;
    
    public WarehouseController(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
    }

    [HttpPost]
    public async Task<IActionResult> PostWarehouseData(Warehouse warehouse)
    {
        var result = _warehouseService.AddProductToWarehouse(warehouse);

        if (result == 1)
        {
            return Ok($"Warehouse added to db with values IdProduct:{warehouse.IdProduct},IdWarehouse: {warehouse.IdWarehouse}, Amount:{warehouse.Amount}, Date:{warehouse.CreatedAt}");
        }

        return NotFound($"Something went wrong when adding Warehouse with sd {warehouse.IdWarehouse} to db");
    }
}