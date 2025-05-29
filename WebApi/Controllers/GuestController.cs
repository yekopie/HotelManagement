using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Dtos.HotelDtos;
using Microsoft.EntityFrameworkCore;
using DataAccess.UnitOfWork;
using WebApi.Dtos.GuestDtos;
using AutoMapper;
using Entities.Concrete;

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
    public async Task<IActionResult> GetAllAsync()
    {
        var guests = await _unitOfWork.GuestRepository.GetAllAsync();
        var result = _mapper.Map<List<GuestDto>>(guests);
        return Ok(result);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        var result = _mapper.Map<GuestDto>(guest);
        return Ok(result);
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGuestDto guestDto)
    {
        bool emailExists = await _unitOfWork.GuestRepository.GetQueryable()
            .AnyAsync(g => g.Email == guestDto.Email);

        if (emailExists) // iş kuralı
        {
            return BadRequest("Bu e-posta adresi zaten kayıtlı.");
        }

        var guest = _mapper.Map<Guest>(guestDto);
        await _unitOfWork.GuestRepository.CreateAsync(guest);
        await _unitOfWork.SaveChangesAsync();
        return Ok(guest);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateGuestDto guestDto)
    {
        var existingGuest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (existingGuest == null) // iş kuralı
        {
            return NotFound();
        }
        var guest = _mapper.Map<Guest>(guestDto);
        guest.Id = id; 
        _unitOfWork.GuestRepository.Update(guest);
        await _unitOfWork.SaveChangesAsync();

        return Ok(guest);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var guest = await _unitOfWork.GuestRepository.GetByIdAsync(id);
        if (guest == null)
        {
            return NotFound();
        }
        _unitOfWork.GuestRepository.Delete(guest);
        await _unitOfWork.SaveChangesAsync();

        return Ok("Silindi");
    }
}





