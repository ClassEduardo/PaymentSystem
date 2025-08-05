using PaymentSystem.Application.Interfaces;
using PaymentSystem.Domain.Entities;
using PaymentSystem.Domain.Services;

namespace PaymentSystem.Application.Services;

public class PaymentProcessor : IPaymentProcessor
{
    private readonly Dictionary<string, IPaymentMethodStrategy> _strategies;

    public PaymentProcessor(IEnumerable<IPaymentMethodStrategy> strategies)
    {
        _strategies = strategies.ToDictionary(s => s.PaymentMethod);
    }

    public async Task<string> ProcessAsync(PaymentRequest request)
    {
        if (!_strategies.TryGetValue(request.Method, out var strategy))
            throw new NotSupportedException($"Método de pagamento '{request.Method}' não é suportado");

        return await strategy.ProcessAsync(request);
    }
}