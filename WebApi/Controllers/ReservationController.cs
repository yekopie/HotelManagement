using AutoMapper;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    //private IUnitOfWork _unitOfWork;
    //private readonly IMapper _mapper;

    //public ReservationController(IUnitOfWork unitOfWork, IMapper mapper)
    //{
    //    _unitOfWork = unitOfWork;
    //    _mapper = mapper;
    //}
    //[HttpGet("filtreli")]
    //public async Task<IActionResult> GetFilteredReservations([FromQuery] string? filter = null)
    //{
    //    var reservationsQuery = _unitOfWork.ReservationRepository.GetFilteredQueryable(r =>
    //        r.Guest.FirstName.ToLower().Contains((filter ?? string.Empty).ToLower()));

    //    var reservations = reservationsQuery.ToList();

    //    if (!reservations.Any())
    //    {
    //        return NotFound("Filtreye uygun rezervasyon bulunamadı.");
    //    }

    //    return Ok();
    //}
    //[HttpGet("{id}")]
    //public async Task<IActionResult> GetReservationById(int id)
    //{
    //    var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);
    //    if (reservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    return Ok(_mapper.Map<ReservationDto>(reservation));
    //}
    //[HttpPost]
    //public async Task<IActionResult> CreateReservation([FromBody] ReservationDto reservationDto)
    //{
    //    if (reservationDto == null)
    //    {
    //        return BadRequest("Rezervasyon bilgileri eksik.");
    //    }
    //    var reservation = _mapper.Map<Entities.Concrete.Reservation>(reservationDto);
    //    await _unitOfWork.ReservationRepository.CreateAsync(reservation);
    //    await _unitOfWork.SaveChangesAsync();
    //    return CreatedAtAction(nameof(GetReservationById), new { id = reservation.Id }, _mapper.Map<ReservationDto>(reservation));
    //}
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateReservation(int id, [FromBody] ReservationDto reservationDto)
    //{
    //    if (reservationDto == null || id != reservationDto.Id)
    //    {
    //        return BadRequest("Rezervasyon bilgileri eksik veya ID uyuşmuyor.");
    //    }
    //    var existingReservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);
    //    if (existingReservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    var updatedReservation = _mapper.Map<Entities.Concrete.Reservation>(reservationDto);
    //    _unitOfWork.ReservationRepository.Update(updatedReservation);
    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();
    //}
    //[HttpPut("confirm/{id}")]
    //public async Task<IActionResult> ConfirmReservation(ReservationDto reservationDto)
    //{
    //    if (reservationDto == null)
    //    {
    //        return BadRequest("Rezervasyon bilgileri eksik.");
    //    }
    //    var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservationDto.Id);
    //    if (reservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    reservation.IsConfirmed = true;
    //    _unitOfWork.ReservationRepository.Update(reservation);
    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();

    //}
    //[HttpPut("checkin/{id}")]
    //public async Task<IActionResult> CheckInReservation(ReservationDto reservationDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest("Model henüz başlatılmadı.");
    //    }
    //    var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservationDto.Id);
    //    if (reservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    //aynı oda + aynı tarihte rezervasyon kontrolü yapılabilir
    //    if (reservation.CheckInDate <= DateTime.Now && reservation.CheckOutDate >= DateTime.Now)
    //    {
    //        return BadRequest("Rezervasyon zaten aktif.");
    //    }
    //    reservation.CheckInDate = DateTime.Now;
    //    _unitOfWork.ReservationRepository.Update(reservation);
    //    await _unitOfWork.SaveChangesAsync();

    //    // misafirin aynı otelde ikinci aktif rezervasyonu olamaz
    //    bool hasActiveReservation = await _unitOfWork.ReservationRepository.GetAllFiltered(r => r.GuestId == reservation.GuestId && r.CheckInDate <= DateTime.Now && r.CheckOutDate >= DateTime.Now).AnyAsync();
    //    if (hasActiveReservation)
    //    {
    //        return BadRequest("Misafirin zaten aktif bir rezervasyonu var.");
    //    }
    //    //check-in tarihi bugünün tarihinden önce olamaz    
    //    if (reservation.CheckInDate < DateTime.Now)
    //    {
    //        return BadRequest("Check-in tarihi bugünden önce olamaz.");
    //    }
    //    return NoContent();

    //}
    //[HttpPut("checkout/{id}")]
    //public async Task<IActionResult> CheckOutReservation(ReservationDto reservationDto)
    //{
    //    if (reservationDto == null)
    //    {
    //        return BadRequest("Rezervasyon bilgileri eksik.");
    //    }
    //    var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(reservationDto.Id);
    //    if (reservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    //check-out tarihi bugünün tarihinden önce olamaz
    //    if (reservation.CheckOutDate < DateTime.Now)
    //    {
    //        return BadRequest("Check-out tarihi bugünden önce olamaz.");
    //    }
    //    reservation.CheckOutDate = DateTime.Now;
    //    _unitOfWork.ReservationRepository.Update(reservation);
    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();
    //}
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteReservation(int id)
    //{
    //    var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);
    //    if (reservation == null)
    //    {
    //        return NotFound("Rezervasyon bulunamadı.");
    //    }
    //    // Rezervasyonun silinmeden önce kontrol edilmesi gereken durumlar olabilir, örneğin rezervasyonun onaylanmış olması gibi.
    //    if (reservation.IsConfirmed)
    //    {
    //        return BadRequest("Onaylanmış rezervasyonlar silinemez.");
    //    }
    //    await _unitOfWork.ReservationRepository.GetByIdAsync(id);
    //    _unitOfWork.ReservationRepository.Delete(reservation);
    //    await _unitOfWork.SaveChangesAsync();
    //    return NoContent();
    //}

}
