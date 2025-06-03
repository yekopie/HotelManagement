using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.RoomDtos;
using System.Collections.Immutable;

namespace Business.Services.Concrete
{
    public class RoomService : IRoomService
    {
        public Task<IDataOutcome<RoomDto>> CreateRoomAsync(int hotelId, CreateRoomDto newRoom)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> DeleteRoomAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IEnumerable<RoomDto>>> GetAvailableRoomsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<RoomDto>> GetRoomByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IEnumerable<RoomDto>>> GetRoomsByHotelIdAsync(int hotelId)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> PatchRoomAsync(int id, UpdateRoomDto patchRoom)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> UpdateRoomAsync(int id, UpdateRoomDto updatedRoom)
        {
            throw new NotImplementedException();
        }
    }
}
