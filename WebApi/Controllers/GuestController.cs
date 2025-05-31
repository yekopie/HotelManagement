using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.GuestDtos;
using AutoMapper;
using Entities.Concrete;
using DataAccess.UnitOfWork;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using WebApi.ValidatonRules;
using Swashbuckle.AspNetCore.Annotations;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GuestsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Tüm misafirleri getirir", Description = "Veritabanındaki tüm misafirleri listeler.")]
    [SwaggerResponse(200, "Başarılı istek", typeof(List<GuestDto>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var guests = await _unitOfWork.GuestRepository.GetAllAsync();
        var result = _mapper.Map<List<GuestDto>>(guests);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "ID'ye göre misafir getirir", Description = "Belirtilen ID'ye sahip misafiri getirir.")]
    [SwaggerResponse(200, "Misafir bulundu", typeof(GuestDto))]
    [SwaggerResponse(404, "Misafir bulunamadı")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (guest == null)
            throw new NotFoundException($"{id} ID'li misafir bulunamadı");

        var guestDto = _mapper.Map<GuestDto>(guest);
        return Ok(guestDto);
    }

    [HttpGet("{id}/reservations")]
    [SwaggerOperation(Summary = "Misafirin rezervasyonlarını getirir", Description = "Belirtilen ID'ye sahip misafirin rezervasyon bilgileriyle birlikte döner.")]
    [SwaggerResponse(404, "Belirtilen ID'ye sahip misafir bulunamadı")]
    public async Task<IActionResult> GetGuestReservationsByIdAsync(int id)
    {
        var guestReservations = await _unitOfWork.GuestRepository
            .GetQueryable()
            .Include(g => g.Reservations)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (guestReservations == null)
            throw new NotFoundException($"{id} ID'li misafir bulunamadı.");

        var guestReservationsDto = _mapper.Map<GuestWithReservationsDto>(guestReservations);
        return Ok(guestReservationsDto);
    }
    [HttpGet("paged")]
    [SwaggerOperation(Summary = "Misafirleri sayfalayarak getirir", Description = "Belirtilen ID'ye sahip misafirin rezervasyon bilgileriyle birlikte döner.")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int currentPage, [FromQuery] int size)
    {
        (IEnumerable<Guest> guests, int totalCount) = await _unitOfWork.GuestRepository.GetPagedAsync(currentPage, size);

        var guestDtos = _mapper.Map<List<GuestDto>>(guests);
        return Ok(new { guestDtos, totalCount});
    }
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreateGuestDto>))]
    [SwaggerOperation(Summary = "Yeni misafir oluşturur", Description = "Yeni bir misafir kaydı oluşturur.")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGuestDto guestDto)
    {
        var result = BusinessRules.Run(CheckEmailIsUnique(guestDto.Email));
        if (!result.IsSuccessfull())
            throw new DomainRuleException(result.Message);

        var guest = _mapper.Map<Guest>(guestDto);
        await _unitOfWork.GuestRepository.CreateAsync(guest);
        await _unitOfWork.SaveChangesAsync();

        _mapper.Map(guestDto, guest);
        return Ok(guestDto);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter<UpdateGuestDto>))]
    [SwaggerOperation(Summary = "Misafir bilgilerini günceller", Description = "Verilen ID'deki misafirin bilgilerini günceller.")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateGuestDto updateGuestDto)
    {
        var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (guest == null) throw new NotFoundException($"{id} ID'li misafir bulunamadı");

        if (updateGuestDto.Email != guest.Email)
        {
            var result = BusinessRules.Run(CheckEmailIsUnique(updateGuestDto.Email));
            if (!result.IsSuccessfull()) throw new DomainRuleException(result.Message);

            _mapper.Map(updateGuestDto, guest);
            _unitOfWork.GuestRepository.Update(guest);
            await _unitOfWork.SaveChangesAsync();
        }
        var guestDto = _mapper.Map<GuestDto>(guest);
        return Ok(guestDto);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Misafiri siler", Description = "Belirtilen ID'deki misafiri siler.")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (guest == null) throw new NotFoundException($"{id}'li misafir bulunamadı");

        _unitOfWork.GuestRepository.Delete(guest);
        await _unitOfWork.SaveChangesAsync();

        return Ok("misafir başarıyla silindi");
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Misafir arar", Description = "İsim, email veya telefon bilgisine göre misafir arar.")]
    public async Task<IActionResult> SearchAsync([FromQuery] string? query)
    {
        var guests = await _unitOfWork.GuestRepository
            .GetQueryable()
            .Where(g =>
                (g.FirstName + " " + g.LastName).Contains(query ?? "") ||
                g.Email.Contains(query ?? "") ||
                g.Phone.Contains(query ?? "")
            ).ToListAsync();

        var guestDtos = _mapper.Map<List<GuestDto>>(guests);
        return Ok(guestDtos);
    }

    /// <summary>
    /// Daha önceden eklenmiş böyle bir email adresi var mı ?
    /// </summary>
    private IOutcome CheckEmailIsUnique(string email)
    {
        bool exists = _unitOfWork.GuestRepository.Any(g => g.Email == email);

        return exists
            ? new ErrorOutcome("Bu e-posta adresi zaten kayıtlı.")
            : new SuccessOutcome();
    }
}
