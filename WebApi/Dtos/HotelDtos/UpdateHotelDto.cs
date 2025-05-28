using Entities.Concrete;

namespace WebApi.Dtos
{
    public class UpdateHotelDto
    {
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int StarRating { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<Room> Rooms { get; set; }
    }
}

