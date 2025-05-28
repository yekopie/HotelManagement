using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Dtos.HotelDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelController : ControllerBase
{
    private IUnitOfWork _unitOfWork;

    public HotelController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var hotels = await _unitOfWork.HotelRepository.GetAll().ToListAsync();
        return Ok(hotels);
    }

    [HttpGet]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        var hotel = await _unitOfWork.HotelRepository.GetByIdAsync(id);
        return Ok(hotel);
    }

    [HttpPost]
    public async Task<IActionResult> AddHotelAsync([FromBody] CreateHotelDto hotel)
    {
        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> AddHotelAsync([FromBody] UpdateHotelDto hotel)
    {
        return Ok();
    }