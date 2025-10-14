using OrderService.Application.Models;

namespace OrderService.Application.Repositories;

public class OrderRepository : IOrderRepository
{
    public void CreateOrder(Order order)
    {
        throw new NotImplementedException();
    }

    public Order GetOrderById(Guid id)
    {
        throw new NotImplementedException();
    }
}