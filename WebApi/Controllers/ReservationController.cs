using AutoMapper;
using Business.Services.Abstract;
using Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet()]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _reservationService.GetAllReservationsAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _reservationService.GetReservationByIdAsync(id);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateReservationDto dto)
    {
        var result = await _reservationService.CreateReservationAsync(dto);
        return CreatedAtAction(nameof(GetByIdAsync), new { id = result.Data.Id }, result);
        //return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateReservationDto dto)
    {
        var result = await _reservationService.UpdateReservationAsync(id, dto);
        return Ok(result);
    }

    [HttpPost("{id}/confirm")]
    public async Task<IActionResult> ConfirmReservationAsync(int id)
    {
        var result = await _reservationService.ConfirmReservationAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/checkin")]
    public async Task<IActionResult> CheckInReservationAsync(int id)
    {
        var result = await _reservationService.CheckInReservationAsync(id);
        return Ok(result);
    }

    [HttpPost("{id}/checkout")]
    public async Task<IActionResult> CheckOutReservationAsync(int id)
    {
        var result = await _reservationService.CheckOutReservationAsync(id);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _reservationService.DeleteReservationAsync(id);
        return NoContent();
    }
}
