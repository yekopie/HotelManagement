using WebApi.Dtos.MaintenanceDtos;
using WebApi.Dtos.ReservationDtos;

namespace WebApi.Dtos.RoomDtos
{
    public class RoomWithReservationsDto : RoomDto
    {
        public List<ReservationDto> Reservations { get; set; }
    }

}


