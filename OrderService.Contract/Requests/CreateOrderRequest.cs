namespace OrderService.Contract.Requests;

public class CreateOrderRequest
{
    public required Guid CustomerId { get; set; }
    public required Guid ProductId { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}