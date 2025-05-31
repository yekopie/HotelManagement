using Entities.Concrete;
using WebApi.Dtos.RoomDtos;

namespace WebApi.Dtos.RoomTypeDtos
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
    }
}
