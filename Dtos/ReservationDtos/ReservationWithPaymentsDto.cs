using Dtos.PaymentDtos;

namespace Dtos.ReservationDtos
{
    public class ReservationWithPaymentsDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string Status { get; set; }
        public List<PaymentDto> Payments { get; set; }
    }
   

}
