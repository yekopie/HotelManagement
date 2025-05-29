using AutoMapper;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Dtos.HotelDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public HotelsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var hotels = await _unitOfWork.HotelRepository.GetAll().ToListAsync();
        var hotelDtos= _mapper.Map<List<HotelDto>>(hotels);
        return Ok(hotelDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id);
        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return Ok(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotel([FromBody] CreateHotelDto hotel)
    {
        var CreatedHotel = _mapper.Map<Hotel>(hotel);
        await _unitOfWork.HotelRepository.CreateAsync(CreatedHotel);
        await _unitOfWork.SaveChangesAsync();
        var hotelDto = _mapper.Map<HotelDto>(CreatedHotel);
        return Ok(CreatedHotel);
    }

    [HttpPut("{id}")]
    public IActionResult AddHotel([FromRoute] int id, [FromBody] UpdateHotelDto hotel)
    {
        return Ok();
    }
    [HttpDelete("{id}")]
    public IActionResult DeleteHotel([FromRoute] int id)
    {
        return Ok();
    }


}