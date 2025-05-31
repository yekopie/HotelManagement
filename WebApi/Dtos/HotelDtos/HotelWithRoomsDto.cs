using WebApi.Dtos.RoomDtos;

namespace WebApi.Dtos.HotelDtos
{
    public class HotelWithRoomsDto : HotelDto
    {
        public List<RoomDto> Rooms { get; set; }
    }
}

