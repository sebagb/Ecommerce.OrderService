using System.Text.Json;
using OrderService.Application.Services;
using OrderService.Contract.Requests;

namespace OrderService.Api;

public static class ApiEnpoints
{
    private const string Base = "orders";
    private readonly static string Create = $"{Base}";
    private readonly static string GetById = $"{Base}/{{id}}";

    public static void RegisterOrderEndpoints
        (this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Create, CreateOrder);
        builder.MapGet(GetById, GetOrderById);
    }

    private static async Task<IResult> CreateOrder(
        HttpRequest request, IOrderService service)
    {
        CreateOrderRequest? createOrderRequest;
        try
        {
            createOrderRequest =
                await request.ReadFromJsonAsync<CreateOrderRequest>();
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is InvalidOperationException)
            {
                return Results.BadRequest($"{ex.Message}");
            }
            throw;
        }

        var order = createOrderRequest!.MapToOrder();

        service.CreateOrder(order);

        var orderResponse = order.MapToResponse();
        return Results.Ok(orderResponse);
    }

    private static IResult GetOrderById(Guid id, IOrderService service)
    {
        var order = service.GetOrderById(id);
        if (order == null)
        {
            return Results.NoContent();
        }

        var response = order.MapToResponse();
        return Results.Ok(response);
    }
}