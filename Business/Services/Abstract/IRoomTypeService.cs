using Core.Utilities.Results.Abstract;
using Dtos.RoomTypeDtos;
using System.Collections.Immutable;

namespace Business.Services.Abstract
{
    public interface IRoomTypeService
    {
        Task<IDataOutcome<IReadOnlyList<RoomTypeDto>>> GetAllRoomTypesAsync();
        Task<IDataOutcome<RoomTypeDto>> GetRoomTypeByIdAsync(int id);
        Task<IDataOutcome<RoomTypeDto>> CreateRoomTypeAsync(CreateRoomTypeDto newRoomType);
        Task<IOutcome> UpdateRoomTypeAsync(int id, UpdateRoomTypeDto updatedRoomType);
        Task<IOutcome> DeleteRoomTypeAsync(int id);
    }
}
