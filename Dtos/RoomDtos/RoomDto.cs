namespace Dtos.RoomDtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public int RoomTypeId { get; set; }
        public int floor { get; set; }
        public string RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        public byte IsAvailable { get; set; }
        public byte IsClean { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}


