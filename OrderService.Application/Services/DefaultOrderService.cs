using OrderService.Application.Models;
using OrderService.Application.Repositories;

namespace OrderService.Application.Services;

public class DefaultOrderService(IOrderRepository repository) : IOrderService
{
    private readonly IOrderRepository repository = repository;
    public void CreateOrder(Order order)
    {
        repository.CreateOrder(order);
    }

    public Order? GetOrderById(Guid id)
    {
        return repository.GetOrderById(id);
    }
}