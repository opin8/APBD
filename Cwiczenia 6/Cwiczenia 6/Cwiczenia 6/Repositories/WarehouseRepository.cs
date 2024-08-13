
using System.Data.SqlClient;
using Cwiczenia_6.Models;
namespace Cwiczenia_6.Repositories;

public class WarehouseRepository : IWarehouseRepository
{
    private readonly IConfiguration _configuration;

    public WarehouseRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public int AddProductToWarehouse(Warehouse warehouse)
    {
        using var con = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]);
        con.Open();

        // 1. Sprawdzamy, czy produkt o podanym identyfikatorze istnieje
        using var cmdProduct = new SqlCommand("SELECT COUNT(1) FROM Product WHERE IdProduct = @IdProduct", con);
        cmdProduct.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
        if ((int)cmdProduct.ExecuteScalar() == 0)
        {
            throw new ArgumentException("Product does not exist.");
        }

        // 2. Sprawdzamy, czy magazyn o podanym identyfikatorze istnieje
        using var cmdWarehouse = new SqlCommand("SELECT COUNT(1) FROM Warehouse WHERE IdWarehouse = @IdWarehouse", con);
        cmdWarehouse.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
        if ((int)cmdWarehouse.ExecuteScalar() == 0)
        {
            throw new ArgumentException("Warehouse does not exist.");
        }

        // 3. Wartość ilości przekazana w żądaniu powinna być większa niż 0
        if (warehouse.Amount <= 0)
        {
            throw new ArgumentException("Amount must be greater than zero.");
        }

        // 4. Sprawdzamy, czy istnieje odpowiednie zamówienie
        using var cmdOrder = new SqlCommand(
            "SELECT TOP 1 IdOrder, IdProduct, Amount, CreatedAt, FulfilledAt " +
            "FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount AND CreatedAt < @CreatedAt AND FulfilledAt IS NULL",
            con);
        cmdOrder.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
        cmdOrder.Parameters.AddWithValue("@Amount", warehouse.Amount);
        cmdOrder.Parameters.AddWithValue("@CreatedAt", warehouse.CreatedAt);
        Order order = null;
        using (var dr = cmdOrder.ExecuteReader())
        {
            if (dr.Read())
            {
                order = new Order
                {
                    IdOrder = dr.GetInt32(0),
                    IdProduct = dr.GetInt32(1),
                    Amount = dr.GetInt32(2),
                    CreatedAt = dr.GetDateTime(3),
                    FulfilledAt = dr.IsDBNull(4) ? (DateTime?)null : dr.GetDateTime(4),
                };
            }
        }
        if (order == null)
        {
            throw new ArgumentException("Valid order does not exist.");
        }

        // 5. Sprawdzamy, czy zamówienie zostało zrealizowane
        using var cmdCheckOrderFulfillment = new SqlCommand(
            "SELECT COUNT(1) FROM Product_Warehouse WHERE IdOrder = @IdOrder", con);
        cmdCheckOrderFulfillment.Parameters.AddWithValue("@IdOrder", order.IdOrder);
        if ((int)cmdCheckOrderFulfillment.ExecuteScalar() > 0)
        {
            throw new ArgumentException("Order has already been fulfilled.");
        }

        // 6. Aktualizujemy kolumnę FulfilledAt zamówienia na aktualną datę i godzinę
        using var cmdUpdateOrder = new SqlCommand(
            "UPDATE [Order] SET FulfilledAt = @FulfilledAt WHERE IdOrder = @IdOrder", con);
        cmdUpdateOrder.Parameters.AddWithValue("@FulfilledAt", DateTime.Now);
        cmdUpdateOrder.Parameters.AddWithValue("@IdOrder", order.IdOrder);
        cmdUpdateOrder.ExecuteNonQuery();

        // 7. Wstawiamy rekord do tabeli Product_Warehouse
        using var cmdInsertProductWarehouse = new SqlCommand(
            "INSERT INTO Product_Warehouse (IdProduct, IdWarehouse, Amount, Price, CreatedAt, IdOrder) " +
            "VALUES (@IdProduct, @IdWarehouse, @Amount, @Price, @CreatedAt, @IdOrder); SELECT SCOPE_IDENTITY();",
            con);
        cmdInsertProductWarehouse.Parameters.AddWithValue("@IdProduct", warehouse.IdProduct);
        cmdInsertProductWarehouse.Parameters.AddWithValue("@IdWarehouse", warehouse.IdWarehouse);
        cmdInsertProductWarehouse.Parameters.AddWithValue("@Amount", warehouse.Amount);
        cmdInsertProductWarehouse.Parameters.AddWithValue("@Price", order.Amount * GetProductPrice(warehouse.IdProduct, con));
        cmdInsertProductWarehouse.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
        cmdInsertProductWarehouse.Parameters.AddWithValue("@IdOrder", order.IdOrder);

        return Convert.ToInt32(cmdInsertProductWarehouse.ExecuteScalar());
    }

    private decimal GetProductPrice(int productId, SqlConnection con)
    {
        using var cmdPrice = new SqlCommand("SELECT Price FROM Product WHERE IdProduct = @IdProduct", con);
        cmdPrice.Parameters.AddWithValue("@IdProduct", productId);
        return (decimal)cmdPrice.ExecuteScalar();
    }
}