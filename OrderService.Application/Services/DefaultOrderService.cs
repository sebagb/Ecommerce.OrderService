using OrderService.Application.MessageQueueing;
using OrderService.Application.Models;
using OrderService.Application.Repositories;

namespace OrderService.Application.Services;

public class DefaultOrderService
    (IOrderRepository repository,
    OrderCreatedProducer producer)
    : IOrderService
{
    private readonly IOrderRepository repository = repository;
    private readonly OrderCreatedProducer producer = producer;

    public void CreateOrder(Order order)
    {
        repository.CreateOrder(order);

        producer.Publish(order.OrderId).Wait();
    }

    public Order? GetOrderById(Guid id)
    {
        return repository.GetOrderById(id);
    }

    public void UpdateOrderStatus(Guid id)
    {
        repository.UpdateOrderStatus(id);
    }
}