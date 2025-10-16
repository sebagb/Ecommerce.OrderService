using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Database;
using OrderService.Application.MessageQueueing;
using OrderService.Application.Repositories;
using OrderService.Application.Services;

namespace OrderService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication
        (this IServiceCollection service, string connectionString)
    {
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddSingleton<IDbConnectionFactory>(_ =>
            new MySqlConnectionFactory(connectionString));

        service.AddSingleton<DbInitializer>();

        service.AddScoped<IOrderService, DefaultOrderService>();
        return service;
    }

    public static IServiceCollection AddMessageQueueing
        (this IServiceCollection service,
        string hostName,
        string orderCreatedQueue)
    {
        service.AddScoped(_ =>
            new OrderCreatedProducer(hostName, orderCreatedQueue));

        return service;
    }
}