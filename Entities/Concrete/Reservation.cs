namespace Entities.Concrete
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public int GuestId { get; set; }
        public virtual Guest Guest { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public ReservationStatus Status { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }

}
