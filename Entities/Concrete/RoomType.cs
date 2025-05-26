namespace Entities.Concrete
{
    public class RoomType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }
    }   

}
