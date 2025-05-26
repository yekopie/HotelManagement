namespace Entities.Concrete
{
    public class Maintenance
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public virtual Room Room { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Note { get; set; }
    }

}
