namespace WebApi.Dtos.PaymentDtos
{
    public class CreatePaymentDto
    {
        public int ReservationID { get; set; }
        public decimal Amount { get; set; }
        public string Method { get; set; }
        public DateTime PaidAt { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
