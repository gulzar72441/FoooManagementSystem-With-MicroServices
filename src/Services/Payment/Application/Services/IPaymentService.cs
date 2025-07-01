using FoodOrderingSystem.Payment.Application.Contracts;

namespace FoodOrderingSystem.Payment.Application.Services;

public interface IPaymentService
{
    Task<PaymentDto> ProcessPaymentAsync(CreatePaymentDto dto);
    Task<PaymentDto> GetPaymentByIdAsync(Guid paymentId);
        Task<IEnumerable<PaymentDto>> GetPaymentsByOrderIdAsync(Guid orderId);
    Task<TransactionDto> GetTransactionByIdAsync(Guid transactionId);
    Task<IEnumerable<TransactionDto>> GetTransactionsByPaymentIdAsync(Guid paymentId);
    Task<RefundDto> GetRefundByIdAsync(Guid refundId);
    Task<IEnumerable<RefundDto>> GetRefundsByPaymentIdAsync(Guid paymentId);
    Task<CommissionDto> GetCommissionByIdAsync(Guid commissionId);
    Task<IEnumerable<CommissionDto>> GetCommissionsByOrderIdAsync(Guid orderId);
    Task<RefundDto> CreateRefundAsync(CreateRefundDto createRefundDto);
} 