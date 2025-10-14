namespace OrderService.Contract.Responses;

public class OrderResponse
{
    public required Guid OrderId { get; set; }
    public required Guid CustomerId { get; set; }
    public required Guid ProductId { get; set; }
    public required DateTime OrderedOn { get; set; }
    public required decimal Price { get; set; }
    public required int Quantity { get; set; }
}