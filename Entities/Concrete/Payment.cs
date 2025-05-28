namespace Entities.Concrete
{
    public class Payment
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public virtual Reservation Reservation { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsSuccessful{ get; set; }
    }

}
