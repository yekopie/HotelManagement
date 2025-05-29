namespace Entities.Concrete
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public virtual Hotel Hotel { get; set; }
        public int RoomTypeId { get; set; }
        public virtual RoomType RoomType { get; set; }
        public int Floor { get; set; }
        public string RoomNumber { get; set; }
        public decimal PricePerNight { get; set; }
        public byte IsAvailable { get; set; }
        public byte IsClean { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }
    }

}
