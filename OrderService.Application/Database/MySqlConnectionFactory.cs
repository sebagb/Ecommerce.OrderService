using System.Data;
using MySql.Data.MySqlClient;

namespace OrderService.Application.Database;

public class MySqlConnectionFactory
    (string connectionString)
    : IDbConectionFactory
{
    public IDbConnection CreateConnection()
    {
        var connection = new MySqlConnection(connectionString);
        connection.Open();
        return connection;
    }
}