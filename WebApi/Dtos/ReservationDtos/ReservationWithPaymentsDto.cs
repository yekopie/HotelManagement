using Entities.Concrete;
using WebApi.Dtos.PaymentDtos;

namespace WebApi.Dtos.ReservationDtos
{
    public class ReservationWithPaymentsDto : ReservationDto
    {
        public List<PaymentDto> Payments { get; set; }
    }
   

}
