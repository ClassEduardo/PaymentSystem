using PaymentSystem.Domain.Entities;
using PaymentSystem.Domain.Services;

namespace PaymentSystem.Infrastructure.Payments;

public class CryptoPaymentStrategy : IPaymentMethodStrategy
{
    public string PaymentMethod => "crypto";

    public Task<string> ProcessAsync(PaymentRequest request)
    {
        return Task.FromResult($"[CRYPTO] Processado R${request.Amount}");
    }
}