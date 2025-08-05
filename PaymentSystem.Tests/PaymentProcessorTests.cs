using Xunit;
using Moq;
using Microsoft.Extensions.DependencyInjection;
using PaymentSystem.Domain.Entities;
using PaymentSystem.Domain.Services;
using PaymentSystem.Application.Interfaces;
using PaymentSystem.Application.Services;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PaymentSystem.Tests;

public class PaymentProcessorTests
{
    private readonly IServiceProvider _provider;

    public PaymentProcessorTests()
    {
        // SetupId para os teste
        var services = new ServiceCollection();

        var creditCardStrategyMock = new Mock<IPaymentMethodStrategy>();

        creditCardStrategyMock.SetupGet(s => s.PaymentMethod).Returns("credit-card");
        creditCardStrategyMock.Setup(s = s.ProcessAsync(It.IsAny<PaymentRequest>()))
                              .ReturnsAsync("CARTÃO TESTADO");

        var pixStrategyMock = new Mock<IPaymentMethodStrategy>();

        pixStrategyMock.SetupGet(s => s.PaymentMethod).Returns("pix");
        pixStrategyMock.Setup(s => s.ProcessAsync(It.IsAny<PaymenteRequest>()))
                       .ReturnsAsync("PIX TESTADO");


        // Registrar mocks no ID contrainer como instâncias
        services.AddSingleton(creditCardStrategyMock.Object);
        services.AddSingleton(pixStrategyMock.Object);

        services.AddScoped<IPaymentProcessor, PaymentProcessor>();
        _provider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task Deve_Processar_Pagamento_Cartao()
    {
        var processor = _provider.GetRequiredService<IPaymentProcessor>();

        var request = new PaymentRequest
        {
            Method = "credit-card",
            Amount = 123.45M
        };

        var result = await processor.ProcessAsync(request);

        Assert.Equal("CARTÃO TESTADO", result);
    }

    [Fact]
    public async Task Deve_Disparar_Excecao_Quando_Estrategia_Nao_Encontrada()
    {
        var processor = _provider.GetRequiredService<IPaymentProcessor>();

        var request = new PaymentRequest
        {
            Method = "crypto",
            Amount = 500

        };

        var ex = await Task.ThrowAsync<NotSupportedException>(() =>
            processor.ProcessAsync(request)
        );

        Assert.Contains("não é suportado", ex.message, StringComparison.OrdinalIgnoreCase);
    }
}