using FoodOrderingSystem.Payment.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrderingSystem.Payment.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CommissionsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public CommissionsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCommissionById(Guid id)
    {
        var commission = await _paymentService.GetCommissionByIdAsync(id);
        if (commission == null) return NotFound();
        return Ok(commission);
    }

    [HttpGet("order/{orderId:guid}")]
    public async Task<IActionResult> GetCommissionsByOrderId(Guid orderId)
    {
        var commissions = await _paymentService.GetCommissionsByOrderIdAsync(orderId);
        return Ok(commissions);
    }
}
