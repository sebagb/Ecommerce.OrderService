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
        service.AddSingleton<IOrderRepository, OrderRepository>();
        service.AddSingleton<IDbConnectionFactory>(_ =>
            new MySqlConnectionFactory(connectionString));

        service.AddSingleton<DbInitializer>();

        service.AddSingleton<IOrderService, DefaultOrderService>();
        return service;
    }

    public static IServiceCollection AddMessageQueueing
        (this IServiceCollection service,
        string hostName,
        string orderCreatedQueue,
        string paymentCompleteQueue)
    {
        service.AddSingleton(_ =>
            new OrderCreatedProducer(hostName, orderCreatedQueue));

        service.AddHostedService(config =>
        {
            var orderService = config.GetRequiredService<IOrderService>();
            return new PaymentCompleteConsumer(
                hostName,
                paymentCompleteQueue,
                orderService);
        });
        return service;
    }
}