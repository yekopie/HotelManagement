using WebApi.Dtos.RoomDtos;

namespace WebApi.Dtos.RoomTypeDtos
{
    public class RoomTypeWithRoomsDto : RoomTypeDto
    {
        public List<RoomDto> Rooms { get; set; }
    }
}
