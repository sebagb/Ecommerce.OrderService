using OrderService.Api;
using OrderService.Application;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration["Database:ConnectionString"];

builder.Services.AddOpenApi();
builder.Services.AddApplication(connectionString!);

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.RegisterOrderEndpoints();

app.Run();