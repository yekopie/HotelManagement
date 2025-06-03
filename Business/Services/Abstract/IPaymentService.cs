using Core.Utilities.Results.Abstract;
using Dtos.PaymentDtos;
using System.Collections.Immutable;

namespace Business.Services.Abstract
{
    public interface IPaymentService
    {
        Task<IDataOutcome<PaymentDto>> CreatePaymentAsync(int reservationId, CreatePaymentDto newPayment);
        Task<IDataOutcome<IEnumerable<PaymentDto>>> GetPaymentsByReservationIdAsync(int reservationId);
        Task<IDataOutcome<PaymentDto>> GetPaymentByIdAsync(int id);
    }
}
