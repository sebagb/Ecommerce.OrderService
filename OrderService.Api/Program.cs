using OrderService.Api;
using OrderService.Application;
using OrderService.Application.Database;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["Database:ConnectionString"];

builder.Services
    .AddOpenApi()
    .AddApplication(connectionString!);

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterOrderEndpoints();

app.Services
    .GetRequiredService<DbInitializer>()
    .Initialize();

app.Run();