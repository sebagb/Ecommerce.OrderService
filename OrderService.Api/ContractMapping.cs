using OrderService.Application.Models;
using OrderService.Contract.Requests;
using OrderService.Contract.Responses;

namespace OrderService.Api;

public static class ContractMapping
{
    public static Order MapToOrder(this CreateOrderRequest request)
    {
        return new Order
        {
            OrderId = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            ProductId = request.ProductId,
            OrderedOn = DateTime.Now,
            Price = request.Price,
            Quantity = request.Quantity,
            Status = "Pending"
        };
    }

    public static OrderResponse MapToResponse(this Order order)
    {
        return new OrderResponse
        {
            OrderId = order.OrderId,
            CustomerId = order.CustomerId,
            ProductId = order.ProductId,
            OrderedOn = order.OrderedOn,
            Price = order.Price,
            Quantity = order.Quantity,
            Status = order.Status
        };
    }
}