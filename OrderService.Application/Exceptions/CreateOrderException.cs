namespace OrderService.Application.Exceptions;

public class CreateOrderException : Exception
{
    public CreateOrderException()
    {
    }

    public CreateOrderException(string message)
        : base(message)
    {
    }
}