using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.RoomDtos;
using Entities.Concrete;
using System.Collections.Immutable;
using System.Security.Cryptography;

namespace Business.Services.Concrete
{
    public class RoomService : IRoomService
    {
        //TODO: Çakışan (RoomId, tarih) kayıtları olan odalar hariç tutulur.
        //TODO: Kapasite parametresi varsa RoomType.Capacity ≥ istenen kapasite olmalıdır.
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
