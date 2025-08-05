using PaymentSystem.Application.Interfaces;
using PaymentSystem.Application.Services;
using PaymentSystem.Domain.Services;
using PaymentSystem.Infrastructure.Payments;

var builder = WebApplication.CreateBuilder(args);

// Camada de Application
builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();

// Camada de Intrastructure
builder.Services.AddScoped<IPaymentMethodStrategy, CreditCardPaymentStrategy>();
builder.Services.AddScoped<IPaymentMethodStrategy, PixPaymentStrategy>();
builder.Services.AddScoped<IPaymentMethodStrategy, CryptoPaymentStrategy>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "PaymentSistem";
    config.Version = "v1";
});

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.UseHttpsRedirection();
app.UseAuthentication();
app.MapControllers();

app.Run();