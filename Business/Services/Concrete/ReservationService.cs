using AutoMapper;
using Business.Services.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Constants;
using DataAccess.UnitOfWork;
using Dtos.GuestDtos;
using Dtos.ReservationDtos;
using Entities.Concrete;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Business.Services.Concrete
{
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ReservationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataOutcome<ReservationDto>> GetReservationByIdAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            var dto = _mapper.Map<ReservationDto>(reservation);
            return new SuccessDataOutcome<ReservationDto>(dto);
        }

        public async Task<IDataOutcome<ReservationDto>> CreateReservationAsync(CreateReservationDto newReservation)
        {
            var result = BusinessRules.Run(
                CheckRoomExists(newReservation.RoomId),
                CheckDateConflict(newReservation.RoomId, newReservation.CheckInDate, newReservation.CheckOutDate),
                CheckValidDates(newReservation.CheckInDate, newReservation.CheckOutDate)
            );

            if (!result.IsSuccessfull())
                throw new DomainRuleException(result.Message);


            // Güncel fiyatı hesapla
            decimal totalAmount = await CalculateTotalAmountAsync(
                newReservation.RoomId, newReservation.CheckInDate, newReservation.CheckOutDate);

            var reservationEntity = _mapper.Map<Reservation>(newReservation);
            reservationEntity.TotalAmount = totalAmount;
            reservationEntity.Status = ReservationStatus.Pending;

            await _unitOfWork.ReservationRepository.CreateAsync(reservationEntity);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<ReservationDto>(reservationEntity);
            return new SuccessDataOutcome<ReservationDto>(dto);
        }

        public async Task<IOutcome> UpdateReservationAsync(int id, UpdateReservationDto updatedReservation)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            // Tarih ve oda değişmişse tarih çakışması kontrolü yap
            if (reservation.RoomId != updatedReservation.RoomId ||
                reservation.CheckInDate != updatedReservation.CheckInDate ||
                reservation.CheckOutDate != updatedReservation.CheckOutDate)
            {
                var result = BusinessRules.Run(
                    CheckRoomExists(updatedReservation.RoomId),
                    CheckDateConflict(updatedReservation.RoomId, updatedReservation.CheckInDate, updatedReservation.CheckOutDate),
                    CheckValidDates(updatedReservation.CheckInDate, updatedReservation.CheckOutDate)
                );
                if (!result.IsSuccessfull())
                    throw new DomainRuleException(result.Message);
            }

            // Güncel fiyatı hesapla
            reservation.TotalAmount = await CalculateTotalAmountAsync(
                updatedReservation.RoomId, updatedReservation.CheckInDate, updatedReservation.CheckOutDate);

            // Mapper ile güncelle
            _mapper.Map(updatedReservation, reservation);

            await _unitOfWork.SaveChangesAsync();
            return new SuccessOutcome("Rezervasyon güncellendi.");
        }

        public async Task<IOutcome> ConfirmReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            if (reservation.Status != ReservationStatus.Pending)
                throw new DomainRuleException("Sadece bekleyen rezervasyonlar onaylanabilir.");

            reservation.Status = ReservationStatus.Confirmed;
            await _unitOfWork.SaveChangesAsync();

            return new SuccessOutcome("Rezervasyon onaylandı.");
        }

        public async Task<IOutcome> CheckInReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            if (reservation.Status != ReservationStatus.Confirmed)
                throw new DomainRuleException("Sadece onaylanmış rezervasyonlarda giriş yapılabilir.");

            reservation.Status = ReservationStatus.CheckedIn;
            await _unitOfWork.SaveChangesAsync();

            return new SuccessOutcome("Giriş yapıldı.");
        }

        public async Task<IOutcome> CheckOutReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            if (reservation.Status != ReservationStatus.CheckedIn)
                throw new DomainRuleException("Sadece giriş yapılmış rezervasyonlarda çıkış yapılabilir.");

            reservation.Status = ReservationStatus.CheckedOut;

            // İptal politikası ve ceza uygulaması burada yapılabilir
            // Örnek: Penalty hesapla ve uygula (opsiyonel)

            await _unitOfWork.SaveChangesAsync();

            return new SuccessOutcome("Çıkış yapıldı.");
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id)
                              ?? throw new NotFoundException($"{id} ID'li rezervasyon bulunamadı.");

            _unitOfWork.ReservationRepository.Delete(reservation);
            await _unitOfWork.SaveChangesAsync();
        }

        private IOutcome CheckRoomExists(int roomId)
        {
            var room = _unitOfWork.RoomRepository.GetByIdAsync(roomId).Result;
            if (room == null)
                throw new NotFoundException("Oda bulunamadı");
            return Outcomes.Success;
        }

        private IOutcome CheckDateConflict(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var conflict = _unitOfWork.ReservationRepository.GetQueryable()
                .Any(r => r.RoomId == roomId &&
                          r.Status != ReservationStatus.Cancelled &&
                          !(checkOut <= r.CheckInDate || checkIn >= r.CheckOutDate));
            if (conflict)
                new ErrorOutcome("Belirtilen tarih aralığı için oda zaten rezerve edildi.");
            return Outcomes.Success;
        }

        private IOutcome CheckValidDates(DateTime checkIn, DateTime checkOut)
        {
            if ((checkOut - checkIn).Days <= 0)
                return new ErrorOutcome("Çıkış tarihi check-in(giriş) tarihinden sonra olmalıdır.");
            return Outcomes.Success;
        }

        private async Task<decimal> CalculateTotalAmountAsync(int roomId, DateTime checkIn, DateTime checkOut)
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(roomId)
                       ?? throw new NotFoundException("Oda bulunamadı.");
            int nights = (checkOut - checkIn).Days;
            return room.PricePerNight * nights;
        }

        public async Task<IDataOutcome<List<ReservationDto>>> GetAllReservationsAsync()
        {
            var reservations = await _unitOfWork.ReservationRepository.GetAllAsync();
            return new SuccessDataOutcome<List<ReservationDto>>(_mapper.Map<List<ReservationDto>>(reservations));
        }
    }
}
