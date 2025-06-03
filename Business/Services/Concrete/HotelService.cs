using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.HotelDtos;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    public partial class HotelService : IHotelService
    {
        public Task<IDataOutcome<HotelDto>> CreateHotelAsync(CreateHotelDto newHotel)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> DeleteHotelAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IEnumerable<HotelDto>>> GetAllHotelsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<HotelDto>> GetHotelByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> UpdateHotelAsync(int id, UpdateHotelDto updatedHotel)
        {
            throw new NotImplementedException();
        }
    }
}
