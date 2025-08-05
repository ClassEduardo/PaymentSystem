namespace PaymentSystem.Domain.Entities;
public class PaymentRequest
{
    public string Method { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}