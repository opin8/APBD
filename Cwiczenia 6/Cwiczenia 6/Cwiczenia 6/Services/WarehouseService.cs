using Cwiczenia_6.Models;
using Cwiczenia_6.Repositories;

namespace Cwiczenia_6.Services;

public class WarehouseService : IWarehouseService
{

    private readonly IWarehouseRepository _warehouseRepository;

    public WarehouseService(IWarehouseRepository warehouseRepository)
    {
        _warehouseRepository = warehouseRepository;
    }

    public int AddProductToWarehouse(Warehouse warehouse)
    {
        return _warehouseRepository.AddProductToWarehouse(warehouse);
    }
}