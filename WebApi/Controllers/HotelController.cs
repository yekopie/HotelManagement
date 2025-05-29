using AutoMapper;
using AutoMapper.QueryableExtensions;
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
        var hotels = await _unitOfWork.HotelRepository.GetAllAsync();
        var hotelDtos = _mapper.Map<List<HotelDto>>(hotels);
        return Ok(hotelDtos);
    }

    [HttpGet("{id}/rooms")]
    public async Task<IActionResult> GetHotelRoomsAsync([FromRoute] int id)
    {
        var rooms = await _unitOfWork.RoomRepository.GetAllByFilterAsync(r => r.HotelId == id);
        //var hotelDto = _mapper.Map<HotelDto>(hotel);
        return Ok(rooms);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int currentPage, [FromQuery] int size)
    {
        // totalcount 
        (IEnumerable<Hotel> hotels, int totalCount) = await _unitOfWork.HotelRepository.GetPagedAsync(currentPage, size);

        return Ok(new { hotels, totalCount });
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id);
        var hotelDto = _mapper.Map<HotelDto>(hotel);
        return Ok(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotelAsync([FromBody] CreateHotelDto hotel)
    {
        var CreatedHotel = _mapper.Map<Hotel>(hotel);
        await _unitOfWork.HotelRepository.CreateAsync(CreatedHotel);
        await _unitOfWork.SaveChangesAsync();
        var hotelDto = _mapper.Map<HotelDto>(CreatedHotel);
        return Ok(CreatedHotel);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHotel([FromRoute] int id, [FromBody] UpdateHotelDto hotelDto)
    {
        var hotel = _mapper.Map<Hotel>(hotelDto);
        hotel.Id = id;
        _unitOfWork.HotelRepository.Update(hotel);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotelAsync([FromRoute] int id)
    {

        var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id);
        _unitOfWork.HotelRepository.Delete(hotel);
        await _unitOfWork.SaveChangesAsync();

        return Ok();
    }

}