using WebApi.Dtos.MaintenanceDtos;

namespace WebApi.Dtos.RoomDtos
{
    public class RoomWithMaintenancesDto : RoomDto
    {
        public List<MaintenanceDto> Maintenances { get; set; }
    }

}


