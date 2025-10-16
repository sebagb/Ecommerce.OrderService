using OrderService.Api;
using OrderService.Application;
using OrderService.Application.Database;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration["Database:ConnectionString"];

var hostName =
    builder.Configuration["MessageQueuing:HostName"]!;

var orderCreatedQueue =
    builder.Configuration["MessageQueuing:OrderCreatedQueue"]!;

builder.Services
    .AddOpenApi()
    .AddApplication(connectionString!)
    .AddMessageQueueing(
        hostName,
        orderCreatedQueue);

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterOrderEndpoints();

app.Services
    .GetRequiredService<DbInitializer>()
    .Initialize();

app.Run();