using System.Text.Json;
using OrderService.Application.Services;
using OrderService.Contract.Requests;

namespace OrderService.Api;

public static class ApiEnpoints
{
    private const string Base = "orders";
    private readonly static string Create = $"{Base}";
    private readonly static string GetById = $"{Base}/{{id}}";

    private static readonly string jsonBodyKey = "jsonBody";

    public static void RegisterOrderEndpoints(
        this IEndpointRouteBuilder builder)
    {
        builder.MapPost(Create, CreateOrder)
            .AddEndpointFilter(BodyValidationFilter);
        builder.MapGet(GetById, GetOrderById);
    }

    private static async Task<IResult> CreateOrder(
        HttpContext context,
        IOrderService service)
    {
        var request = (CreateOrderRequest)context.Items[jsonBodyKey]!;

        var order = request.MapToOrder();

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

    private static async ValueTask<object?> BodyValidationFilter(
        EndpointFilterInvocationContext context,
        EndpointFilterDelegate next)
    {
        try
        {
            var request = await context.HttpContext.Request
                .ReadFromJsonAsync<CreateOrderRequest>();

            context.HttpContext.Items.Add(jsonBodyKey, request);
        }
        catch (Exception ex)
        {
            if (ex is JsonException || ex is InvalidOperationException)
            {
                return Results.BadRequest(ex.Message);
            }
            throw;
        }

        return await next(context);
    }
}