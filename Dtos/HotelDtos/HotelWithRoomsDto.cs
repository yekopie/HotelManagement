using Dtos.RoomDtos;

namespace Dtos.HotelDtos
{
    public class HotelWithRoomsDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int StarRating { get; set; }
        public List<RoomDto> Rooms { get; set; }
    }
}

