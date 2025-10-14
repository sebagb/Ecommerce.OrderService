using Dapper;

namespace OrderService.Application.Database;

public class DbInitializer(IDbConnectionFactory connectionFactory)
{
    private readonly IDbConnectionFactory connectionFactory = connectionFactory;

    public void Initialize()
    {
        using var connection = connectionFactory.CreateConnection();

        connection.Execute("""
        CREATE TABLE IF NOT EXISTS Orders (
            OrderId BINARY(16) PRIMARY KEY,
            CustomerId BINARY(16),
            ProductId BINARY(16),
            OrderedOn TIMESTAMP,
            Price DECIMAL(7,2),
            Quantity INTEGER
        )
        """);
    }
}