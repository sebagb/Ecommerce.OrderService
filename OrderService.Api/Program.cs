using OrderService.Api;
using OrderService.Application;
using OrderService.Application.Database;

var builder = WebApplication.CreateBuilder(args);

var userApiUri =
    builder.Configuration["ApiServers:UserApiUri"]!;

var connectionString =
    builder.Configuration["Database:ConnectionString"];

var hostName =
    builder.Configuration["MessageQueuing:HostName"]!;

var orderCreatedQueue =
    builder.Configuration["MessageQueuing:OrderCreatedQueue"]!;

var paymentCompletedQueue =
    builder.Configuration["MessageQueuing:PaymentCompletedQueue"]!;

builder.Services
    .AddOpenApi()
    .ConfigureApiClient(userApiUri)
    .AddApplication(connectionString!)
    .AddMessageQueueing(
        hostName,
        orderCreatedQueue,
        paymentCompletedQueue);

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterOrderEndpoints();

app.Services
    .GetRequiredService<DbInitializer>()
    .Initialize();

app.Run();