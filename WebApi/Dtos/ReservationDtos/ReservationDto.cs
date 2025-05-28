using Entities.Concrete;
using WebApi.Dtos.PaymentDtos;

namespace WebApi.Dtos.ReservationDtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
   

}
