using PaymentSystem.Domain.Entities;

namespace PaymentSystem.Domain.Services;

public interface IPaymentMethodStrategy
{
    string PaymentMethod { get; }
    Task<string> ProcessAsync(PaymentRequest request);
}