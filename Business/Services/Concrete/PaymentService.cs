using AutoMapper;
using Business.Services.Abstract;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.UnitOfWork;
using Dtos.PaymentDtos;
using Entities.Concrete;
using System.Collections.Immutable;

namespace Business.Services.Concrete
{
    public class PaymentService : IPaymentService
    {
        //TODO: Ödemenin toplamı TotalAmount değerini aşamaz.
        //TODO: Ödeme sonrası bakiye sıfır ise rezervasyon durumu PaidConfirmed(opsiyonel).
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataOutcome<PaymentDto>> CreatePaymentAsync(int reservationId, CreatePaymentDto newPayment)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservationId)
                              ?? throw new NotFoundException($"{reservationId} ID'li rezervasyon bulunamadı.");

            var payment = _mapper.Map<Payment>(newPayment);
            payment.ReservationId = reservationId;

            await _unitOfWork.PaymentRepository.CreateAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<PaymentDto>(payment);
            return new SuccessDataOutcome<PaymentDto>(dto);
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
