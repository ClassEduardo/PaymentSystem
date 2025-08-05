using PaymentSystem.Domain.Entities;
using PaymentSystem.Domain.Services;

namespace PaymentSystem.Infrastructure.Payments;

public class PixPaymentStrategy : IPaymentMethodStrategy
{
    public string PaymentMethod => "pix";

    public Task<string> ProcessAsync(PaymentRequest request)
    {
        return Task.FromResult($"[PIX] Processado R${request.Amount}");
    }
}