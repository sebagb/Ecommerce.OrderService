namespace OrderService.Application.Models;

public class Order
{
    public required Guid Id { get; set; }
    public required Guid CustomerId { get; set; }
    public required Guid ProductId { get; set; }
    public required DateOnly OrderedOn { get; set; }
    public required int Price { get; set; }
    public required int Quantity { get; set; }
}