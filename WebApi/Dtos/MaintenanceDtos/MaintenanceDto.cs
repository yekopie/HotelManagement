using Entities.Concrete;
using WebApi.Dtos.MaintenanceDtos;

namespace WebApi.Dtos.MaintenanceDtos
{
    public class MaintenanceDto
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Note { get; set; }
    }
}
