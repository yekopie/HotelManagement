using AutoMapper;
using Business.Services.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.UnitOfWork;
using Dtos.HotelDtos;
using Entities.Concrete;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Business.Services.Concrete
{
    //TODO: sayfalı otel listesi eklenecek page size > 1
    public partial class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataOutcome<IEnumerable<HotelDto>>> GetAllHotelsAsync()
        {
            var hotels = await _unitOfWork.HotelRepository.GetAllAsync();
            var dtoList = _mapper.Map<IEnumerable<HotelDto>>(hotels);
            return new SuccessDataOutcome<IEnumerable<HotelDto>>(dtoList);
        }

        public async Task<IDataOutcome<HotelDto>> GetHotelByIdAsync(int id)
        {
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException($"{id} ID'li otel bulunamadı.");

            var dto = _mapper.Map<HotelDto>(hotel);
            return new SuccessDataOutcome<HotelDto>(dto);
        }

        public async Task<IDataOutcome<HotelDto>> CreateHotelAsync(CreateHotelDto newHotel)
        {
            var result = BusinessRules.Run();

            var entity = _mapper.Map<Hotel>(newHotel);
            await _unitOfWork.HotelRepository.CreateAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<HotelDto>(entity);
            return new SuccessDataOutcome<HotelDto>(dto);
        }

        public async Task<IOutcome> UpdateHotelAsync(int id, UpdateHotelDto updatedHotel)
        {
            var result = BusinessRules.Run();
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException($"{id} ID'li otel bulunamadı.");

            _mapper.Map(updatedHotel, hotel);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessOutcome("Otel bilgileri güncellendi.");
        }

        public async Task<IOutcome> DeleteHotelAsync(int id)
        {
            var result = BusinessRules.Run();
            var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id)
                        ?? throw new NotFoundException($"{id} ID'li otel bulunamadı.");

            _unitOfWork.HotelRepository.Delete(hotel);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessOutcome("Otel silindi.");
        }

    }
}
