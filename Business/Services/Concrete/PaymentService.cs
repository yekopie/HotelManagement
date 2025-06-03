using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.PaymentDtos;
using System.Collections.Immutable;

namespace Business.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        public Task<IDataOutcome<PaymentDto>> CreatePaymentAsync(int reservationId, CreatePaymentDto newPayment)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<PaymentDto>> GetPaymentByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IEnumerable<PaymentDto>>> GetPaymentsByReservationIdAsync(int reservationId)
        {
            throw new NotImplementedException();
        }
    }
}
