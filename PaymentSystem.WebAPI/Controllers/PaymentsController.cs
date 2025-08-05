using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.Interfaces;
using PaymentSystem.Domain.Entities;

namespace PaymentSystem.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentProcessor _paymentProcessor;

    public PaymentsController(IPaymentProcessor paymentProcessor)
    {
        _paymentProcessor = paymentProcessor;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] PaymentRequest request)
    {
        try
        {
            var result = await _paymentProcessor.ProcessAsync(request);
            return Ok(result);
        }
        catch (NotSupportedException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}