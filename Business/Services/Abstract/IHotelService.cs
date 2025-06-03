using Core.Utilities.Results.Abstract;
using Dtos.HotelDtos;

namespace Business.Services.Abstract
{
    public interface IHotelService
    {
        Task<IDataOutcome<IEnumerable<HotelDto>>> GetAllHotelsAsync();
        Task<IDataOutcome<HotelDto>> GetHotelByIdAsync(int id);
        Task<IDataOutcome<HotelDto>> CreateHotelAsync(CreateHotelDto newHotel);
        Task<IOutcome> UpdateHotelAsync(int id, UpdateHotelDto updatedHotel);
        Task<IOutcome> DeleteHotelAsync(int id);
    }
}
