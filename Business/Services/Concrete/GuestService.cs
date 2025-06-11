using AutoMapper;
using Business.Services.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Constants;
using DataAccess.UnitOfWork;
using Dtos.GuestDtos;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace Business.Services.Concrete
{
    public class GuestService : IGuestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GuestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<IDataOutcome<List<GuestDto>>> GetAllAsync()
        {
            var guests = await _unitOfWork.GuestRepository.GetAllAsync();
            return new SuccessDataOutcome<List<GuestDto>>(_mapper.Map<List<GuestDto>>(guests));
        }

        public async Task<IDataOutcome<GuestDto>> GetByIdAsync(int id)
        {
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"{id} ID'li misafir bulunamadı");
            return new SuccessDataOutcome<GuestDto>(_mapper.Map<GuestDto>(guest));
        }

        public async Task<IDataOutcome<GuestWithReservationsDto>> GetWithReservationsAsync(int id)
        {
            var guest = await _unitOfWork.GuestRepository
                .GetQueryable()
                .Include(g => g.Reservations)
                .FirstOrDefaultAsync(g => g.Id == id)
                ?? throw new NotFoundException($"{id} ID'li misafir bulunamadı");

            return new SuccessDataOutcome<GuestWithReservationsDto>(_mapper.Map<GuestWithReservationsDto>(guest));
        }

        public async Task<IDataOutcome<(List<GuestDto> Guests, int TotalCount)>> GetPagedAsync(int currentPage, int size)
        {
            var (guests, totalCount) = await _unitOfWork.GuestRepository.GetPagedAsync(currentPage, size);
            return new SuccessDataOutcome<(List<GuestDto>, int)>((_mapper.Map<List<GuestDto>>(guests), totalCount));
        }

        public async Task<IDataOutcome<GuestDto>> CreateAsync(CreateGuestDto dto)
        {
            var result = BusinessRules.Run(IsGuestDuplicate(dto), CheckEmailIsUnique(dto.Email));
            if (!result.IsSuccessfull()) throw new DomainRuleException(result.Message);

            var guest = _mapper.Map<Guest>(dto);
            await _unitOfWork.GuestRepository.CreateAsync(guest);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessDataOutcome<GuestDto>(_mapper.Map<GuestDto>(guest));
        }

        public async Task<IDataOutcome<GuestDto>> UpdateAsync(int id, UpdateGuestDto dto)
        {
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"{id} ID'li misafir bulunamadı");

            if (dto.Email != guest.Email)
            {
                var result = BusinessRules.Run(CheckEmailIsUnique(dto.Email));
                if (!result.IsSuccessfull()) throw new DomainRuleException(result.Message);
            }

            _mapper.Map(dto, guest);
            _unitOfWork.GuestRepository.Update(guest);
            await _unitOfWork.SaveChangesAsync();

            return new SuccessDataOutcome<GuestDto>(_mapper.Map<GuestDto>(guest));
        }

        public async Task DeleteAsync(int id)
        {
            var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id)
                         ?? throw new NotFoundException($"{id} ID'li misafir bulunamadı");

            _unitOfWork.GuestRepository.Delete(guest);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IDataOutcome<List<GuestDto>>> SearchAsync(string? query)
        {
            var guests = await _unitOfWork.GuestRepository
                .GetQueryable()
                .Where(g =>
                    (g.FirstName + " " + g.LastName).Contains(query ?? "") ||
                    g.Email.Contains(query ?? "") ||
                    g.Phone.Contains(query ?? "")
                ).ToListAsync();

            return new SuccessDataOutcome<List<GuestDto>>(_mapper.Map<List<GuestDto>>(guests));
        }

        // Business Rules
        private IOutcome CheckEmailIsUnique(string email)
        {
            bool exists = _unitOfWork.GuestRepository.Any(g => g.Email == email);
            return exists
                ? new ErrorOutcome("Bu e-posta adresi zaten kayıtlı.")
                : Outcomes.Success;
        }

        private IOutcome IsGuestDuplicate(CreateGuestDto guestDto)
        {
            bool exists = _unitOfWork.GuestRepository.Any(g =>
                g.FirstName == guestDto.FirstName &&
                g.LastName == guestDto.LastName &&
                g.Phone == guestDto.Phone);

            return exists 
                ? new ErrorOutcome("Aynı misafiri tekrar kayıt edemezsiniz") 
                : Outcomes.Success;
        }
    }
}
