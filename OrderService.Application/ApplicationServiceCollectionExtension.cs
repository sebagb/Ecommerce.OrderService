using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Database;
using OrderService.Application.Repositories;

namespace OrderService.Application;

public static class ApplicationServiceCollectionExtension
{
    public static IServiceCollection AddApplication
        (this IServiceCollection service, string connectionString)
    {
        service.AddScoped<IOrderRepository, OrderRepository>();
        service.AddScoped<IDbConnectionFactory>(_ =>
            new MySqlConnectionFactory(connectionString));
        service.AddSingleton<DbInitializer>();

        return service;
    }
}