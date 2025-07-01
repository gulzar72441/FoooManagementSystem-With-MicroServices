using FoodOrderingSystem.Payment.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public TransactionsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetTransactionById(Guid id)
    {
        var transaction = await _paymentService.GetTransactionByIdAsync(id);
        if (transaction == null) return NotFound();
        return Ok(transaction);
    }

    [HttpGet("payment/{paymentId:guid}")]
    public async Task<IActionResult> GetTransactionsByPaymentId(Guid paymentId)
    {
        var transactions = await _paymentService.GetTransactionsByPaymentIdAsync(paymentId);
        return Ok(transactions);
    }
}
