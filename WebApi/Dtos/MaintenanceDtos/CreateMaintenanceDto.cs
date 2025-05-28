namespace WebApi.Dtos.MaintenanceDtos
{
    public class CreateMaintenanceDto
    {
        public int RoomId { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime ScheduledDate { get; set; }
        public DateTime CompletedDate { get; set; }
        public string Note { get; set; }
    }
}
