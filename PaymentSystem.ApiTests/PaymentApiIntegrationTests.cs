using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using PaymentSystem.Domain.Entities;
using Xunit;

namespace PaymentSystem.ApiTests;

public class PaymentApiIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;

    public PaymentApiIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _httpClient = factory.CreateClient();
    }

    [Fact]
    public async Task Deve_Retornar_Sucesso_Para_Cartao_Credito()
    {
        var request = new PaymentRequest
        {
            Method = "credit-card",
            Amount = 150.0m
        };

        var response = await _httpClient.PostAsJsonAsync("/api/payments", request);

        Assert.Equals(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();

        Assert.Contains("CARTÃO DE CRÉDITO", content, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public async Task Deve_Retornar_Erro_Para_Metodo_Desconhecido()
    {
        var request = new PaymentRequest
        {
            Method = "dinheiro",
            Amount = 80.0m
        };

        var response = await _client.PostAsJsonAsync("/api/payments", request);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("não é suportado", content, StringComparison.OrdinalIgnoreCase);
    }
}