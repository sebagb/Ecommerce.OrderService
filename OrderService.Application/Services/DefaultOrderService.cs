using OrderService.Application.ApiClients;
using OrderService.Application.MessageQueueing;
using OrderService.Application.Models;
using OrderService.Application.Repositories;

namespace OrderService.Application.Services;

public class DefaultOrderService
    (IOrderRepository repository,
    OrderCreatedProducer producer,
    UserApiClient userApiClient,
    ProductApiClient productApiClient)
    : IOrderService
{
    private readonly IOrderRepository repository = repository;
    private readonly OrderCreatedProducer producer = producer;
    private readonly UserApiClient userApiClient = userApiClient;
    private readonly ProductApiClient productApiClient = productApiClient;

    public void CreateOrder(Order order)
    {
        var user = userApiClient.GetUserById(order.CustomerId);
        var isValidCustomer =
            user != null
            && !string.IsNullOrEmpty(user.Email);

        var productStock = productApiClient.GetProductStock(order.ProductId);
        var enoughStock = order.Quantity <= productStock;

        repository.CreateOrder(order);

        producer.Publish(order.OrderId).Wait();
    }

    public Order? GetOrderById(Guid id)
    {
        return repository.GetOrderById(id);
    }

    public void UpdateOrderStatus(Guid id, string status)
    {
        repository.UpdateOrderStatus(id, status);
    }
}