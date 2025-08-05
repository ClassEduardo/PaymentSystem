using PaymentSystem.Domain.Entities;
using PaymentSystem.Domain.Services;

namespace PaymentSystem.Infrastructure.Payments;

public class CreditCardPaymentStrategy : IPaymentMethodStrategy
{
    public string PaymentMethod => "credit-card";

    public Task<string> ProcessAsync(PaymentRequest request)
    {
        return Task.FromResult($"[CARTÃO] Processado R$ {request.Amount}");
    }
}