using PaymentSystem.Domain.Entities;

namespace PaymentSystem.Application.Interfaces;

public interface IPaymentProcessor
{
    Task<string> ProcessAsync(PaymentRequest request);
}