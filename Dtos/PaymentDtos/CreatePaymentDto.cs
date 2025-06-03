namespace Dtos.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
