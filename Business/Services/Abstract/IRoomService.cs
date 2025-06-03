using Core.Utilities.Results.Abstract;
using Dtos.RoomDtos;
namespace Business.Services.Abstract
{
    public interface IRoomService
    {
        Task<IDataOutcome<IEnumerable<RoomDto>>> GetRoomsByHotelIdAsync(int hotelId);
        Task<IDataOutcome<RoomDto>> GetRoomByIdAsync(int id);
        Task<IDataOutcome<RoomDto>> CreateRoomAsync(int hotelId, CreateRoomDto newRoom);
        Task<IOutcome> UpdateRoomAsync(int id, UpdateRoomDto updatedRoom);
        Task<IOutcome> PatchRoomAsync(int id, UpdateRoomDto patchRoom);  
        Task<IOutcome> DeleteRoomAsync(int id);
        Task<IDataOutcome<IEnumerable<RoomDto>>> GetAvailableRoomsAsync();
    }
}
