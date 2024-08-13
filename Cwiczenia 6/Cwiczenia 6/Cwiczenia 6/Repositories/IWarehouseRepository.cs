using Cwiczenia_6.Controllers;
using Cwiczenia_6.Models;

namespace Cwiczenia_6.Repositories;

public interface IWarehouseRepository
{
    int AddProductToWarehouse(Warehouse warehouse);
    
}