using WebApi.Dtos.ReservationDtos;

namespace WebApi.Dtos.GuestDtos
{
    public class GuestWithReservationsDto : GuestDto
    {
        public List<ReservationDto> Reservations { get; set; }
    }
}
