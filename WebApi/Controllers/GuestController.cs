using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Dtos.HotelDtos;
using Microsoft.EntityFrameworkCore;
using DataAccess.UnitOfWork;
using WebApi.Dtos.GuestDtos;
using AutoMapper;
using Entities.Concrete;
using WebApi.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using WebApi.ValidatonRules;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Business;
using Core.Utilities.Results;

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
    //[ServiceFilter(typeof(ValidationFilter<CreateGuestDto>))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGuestDto guestDto)
    {
        
        var result = BusinessRules.Run(CheckEmailIsUnique(guestDto.Email));
        if (!result.IsSuccessfull()) 
            throw new DomainRuleException(result?.Message ?? "");
        var guest = _mapper.Map<Guest>(guestDto);
        await _unitOfWork.GuestRepository.CreateAsync(guest);
        await _unitOfWork.SaveChangesAsync();
        return Ok(guest);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter<UpdateGuestDto>))]
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

    private IOutcome CheckEmailIsUnique(string email)
    {
        bool hasEmail = _unitOfWork.GuestRepository.GetQueryable().Any(g => g.Email == email);
        
        return hasEmail? new ErrorOutcome("Mail adresi zaten mevcuttur") : new SuccessOutcome();
    }



}





