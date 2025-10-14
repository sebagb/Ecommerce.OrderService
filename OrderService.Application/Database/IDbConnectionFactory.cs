using System.Data;

namespace OrderService.Application.Database;

public interface IDbConectionFactory
{
    public IDbConnection CreateConnection();
}