using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Dtos.GuestDtos;
using WebApi.Filters;
using Business.Services.Abstract;
using Core.Utilities.Exceptions;
using WebApi.Middlewares;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestsController : ControllerBase
{
    private readonly IGuestService _guestService;

    public GuestsController(IGuestService guestService)
    {
        _guestService = guestService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _guestService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _guestService.GetByIdAsync(id);

        return Ok(result);
    }
    [HttpGet("{id}/reservations")]
    public async Task<IActionResult> GetWithReservationsByIdAsync(int id)
    {
        var result = await _guestService.GetWithReservationsAsync(id);
        return Ok(result);
    }
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int currentPage, [FromQuery] int size)
    {
        var result = await _guestService.GetPagedAsync(currentPage, size);
        return Ok(result);
    }
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreateGuestDto>))]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGuestDto newGuestDto)
    {
        var result = await _guestService.CreateAsync(newGuestDto);
        return CreatedAtAction(nameof(GetByIdAsync), "Guests", new { id = result.Data.Id}, result);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter<UpdateGuestDto>))]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateGuestDto updateGuestDto)
    {
        var result = await _guestService.UpdateAsync(id, updateGuestDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _guestService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchAsync([FromQuery] string? query)
    {
        var result = await _guestService.SearchAsync(query);
        return Ok(result);
    }

}
