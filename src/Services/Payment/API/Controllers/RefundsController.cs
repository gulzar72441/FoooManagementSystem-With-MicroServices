using FoodOrderingSystem.Payment.Application.Contracts;
using FoodOrderingSystem.Payment.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class RefundsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public RefundsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRefundById(Guid id)
    {
        var refund = await _paymentService.GetRefundByIdAsync(id);
        if (refund == null) return NotFound();
        return Ok(refund);
    }

    [HttpGet("payment/{paymentId:guid}")]
    public async Task<IActionResult> GetRefundsByPaymentId(Guid paymentId)
    {
        var refunds = await _paymentService.GetRefundsByPaymentIdAsync(paymentId);
        return Ok(refunds);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRefund([FromBody] CreateRefundDto createRefundDto)
    {
        var refund = await _paymentService.CreateRefundAsync(createRefundDto);

        if (refund == null)
        {
            return BadRequest("Could not create refund. The payment may not exist, may not be completed, or the refund amount may be invalid.");
        }

        return CreatedAtAction(nameof(GetRefundById), new { id = refund.Id }, refund);
    }
}
