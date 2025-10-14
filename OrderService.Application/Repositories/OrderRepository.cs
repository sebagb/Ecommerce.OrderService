using Dapper;
using OrderService.Application.Database;
using OrderService.Application.Models;

namespace OrderService.Application.Repositories;

public class OrderRepository
    (IDbConnectionFactory connectionFactory)
    : IOrderRepository
{
    private readonly IDbConnectionFactory connectionFactory = connectionFactory;

    public void CreateOrder(Order order)
    {
        using var connection = connectionFactory.CreateConnection();

        var cmd = new CommandDefinition("""
            INSERT INTO Orders (
                OrderId,
                CustomerId,
                ProductId,
                OrderedOn,
                Price,
                Quantity)
            VALUES (
                @OrderId,
                @CustomerId,
                @ProductId,
                @OrderedOn,
                @Price,
                @Quantity)
            """, order);

        var result = connection.Execute(cmd);
    }

    public Order? GetOrderById(Guid id)
    {
        using var connection = connectionFactory.CreateConnection();

        var cmd = new CommandDefinition("""
            SELECT *
            FROM Orders
            WHERE OrderId = @id
            """, new { id });

        var order = connection.QuerySingleOrDefault<Order>(cmd);

        if (order == null)
        {
            return null;
        }

        return order;
    }
}