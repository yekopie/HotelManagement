using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.RoomTypeDtos;
using System.Collections.Immutable;

namespace Business.Services.Concrete
{
    public class RoomTypeService : IRoomTypeService
    {
        public Task<IDataOutcome<RoomTypeDto>> CreateRoomTypeAsync(CreateRoomTypeDto newRoomType)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> DeleteRoomTypeAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IReadOnlyList<RoomTypeDto>>> GetAllRoomTypesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<RoomTypeDto>> GetRoomTypeByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> UpdateRoomTypeAsync(int id, UpdateRoomTypeDto updatedRoomType)
        {
            throw new NotImplementedException();
        }
    }
}
