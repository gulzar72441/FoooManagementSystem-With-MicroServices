using FoodOrderingSystem.Payment.Application.Contracts;
using FoodOrderingSystem.Payment.Application.Services;
using FoodOrderingSystem.Payment.Domain.Entities;
using FoodOrderingSystem.Payment.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FoodOrderingSystem.Payment.Infrastructure.Services;

public class PaymentService : IPaymentService
{
    private readonly PaymentDbContext _context;

    public PaymentService(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto)
    {
        var payment = new Domain.Entities.Payment
        {
            OrderId = dto.OrderId,
            Amount = dto.Amount,
            PaymentMethod = dto.PaymentMethod,
            Status = "Completed",
            PaymentDate = DateTime.UtcNow
        };

        _context.Payments.Add(payment);

        var transaction = new Transaction
        {
            Payment = payment,
            Amount = dto.Amount,
            Type = "Payment",
            TransactionDate = DateTime.UtcNow,
            ExternalTransactionId = Guid.NewGuid().ToString()
        };
        
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new PaymentDto
        {
            Id = payment.Id,
            OrderId = payment.OrderId,
            Amount = payment.Amount,
            Status = payment.Status,
            PaymentDate = payment.PaymentDate,
            PaymentMethod = payment.PaymentMethod
        };
    }

    public async Task<PaymentDto> GetPaymentByIdAsync(Guid paymentId)
    {
        return await _context.Payments
            .Where(p => p.Id == paymentId)
            .Select(p => new PaymentDto 
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<PaymentDto>> GetPaymentsByOrderIdAsync(Guid orderId)
    {
        return await _context.Payments
            .Where(p => p.OrderId == orderId)
            .Select(p => new PaymentDto
            {
                Id = p.Id,
                OrderId = p.OrderId,
                Amount = p.Amount,
                Status = p.Status,
                PaymentDate = p.PaymentDate,
                PaymentMethod = p.PaymentMethod
            })
            .ToListAsync();
    }

    public async Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId)
    {
        return await _context.Transactions
            .Where(t => t.Id == transactionId)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                PaymentId = t.PaymentId,
                Amount = t.Amount,
                Type = t.Type,
                TransactionDate = t.TransactionDate,
                ExternalTransactionId = t.ExternalTransactionId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<TransactionDto>> GetTransactionsByPaymentIdAsync(Guid paymentId)
    {
        return await _context.Transactions
            .Where(t => t.PaymentId == paymentId)
            .Select(t => new TransactionDto
            {
                Id = t.Id,
                PaymentId = t.PaymentId,
                Amount = t.Amount,
                Type = t.Type,
                TransactionDate = t.TransactionDate,
                ExternalTransactionId = t.ExternalTransactionId
            })
            .ToListAsync();
    }

    public async Task<RefundDto> GetRefundByIdAsync(Guid refundId)
    {
        return await _context.Refunds
            .Where(r => r.Id == refundId)
            .Select(r => new RefundDto
            {
                Id = r.Id,
                PaymentId = r.PaymentId,
                Amount = r.Amount,
                Reason = r.Reason,
                RefundDate = r.RefundDate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<RefundDto>> GetRefundsByPaymentIdAsync(Guid paymentId)
    {
        return await _context.Refunds
            .Where(r => r.PaymentId == paymentId)
            .Select(r => new RefundDto
            {
                Id = r.Id,
                PaymentId = r.PaymentId,
                Amount = r.Amount,
                Reason = r.Reason,
                RefundDate = r.RefundDate
            })
            .ToListAsync();
    }

    public async Task<CommissionDto> GetCommissionByIdAsync(Guid commissionId)
    {
        return await _context.Commissions
            .Where(c => c.Id == commissionId)
            .Select(c => new CommissionDto
            {
                Id = c.Id,
                OrderId = c.OrderId,
                TotalAmount = c.TotalAmount,
                RestaurantCommission = c.RestaurantCommission,
                RiderCommission = c.RiderCommission,
                CalculationDate = c.CalculationDate
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<CommissionDto>> GetCommissionsByOrderIdAsync(Guid orderId)
    {
        return await _context.Commissions
            .Where(c => c.OrderId == orderId)
            .Select(c => new CommissionDto
            {
                Id = c.Id,
                OrderId = c.OrderId,
                TotalAmount = c.TotalAmount,
                RestaurantCommission = c.RestaurantCommission,
                RiderCommission = c.RiderCommission,
                CalculationDate = c.CalculationDate
            })
            .ToListAsync();
    }

    public async Task<RefundDto> CreateRefundAsync(CreateRefundDto createRefundDto)
    {
        var payment = await _context.Payments.FindAsync(createRefundDto.PaymentId);

        if (payment == null || payment.Status != "Completed")
        {
            // Cannot refund a payment that is not completed or does not exist
            return null;
        }

        if (createRefundDto.Amount > payment.Amount)
        {
            // Cannot refund more than the original payment amount
            return null;
        }

        var refund = new Refund
        {
            Id = Guid.NewGuid(),
            PaymentId = createRefundDto.PaymentId,
            Amount = createRefundDto.Amount,
            Reason = createRefundDto.Reason,
            RefundDate = DateTime.UtcNow
        };

        var transaction = new Transaction
        {
            Id = Guid.NewGuid(),
            PaymentId = createRefundDto.PaymentId,
            Amount = -createRefundDto.Amount, // Negative amount for refund transaction
            Type = "Refund",
            TransactionDate = DateTime.UtcNow,
            ExternalTransactionId = Guid.NewGuid().ToString() // Placeholder for external ID
        };

        _context.Refunds.Add(refund);
        _context.Transactions.Add(transaction);

        await _context.SaveChangesAsync();

        return new RefundDto
        {
            Id = refund.Id,
            PaymentId = refund.PaymentId,
            Amount = refund.Amount,
            Reason = refund.Reason,
            RefundDate = refund.RefundDate
        };
    }
} 