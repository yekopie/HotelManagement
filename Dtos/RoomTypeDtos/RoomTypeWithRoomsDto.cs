using Dtos.RoomDtos;

namespace Dtos.RoomTypeDtos
{
    public class RoomTypeWithRoomsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}
