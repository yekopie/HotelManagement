using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Entities.Concrete;
using DataAccess.UnitOfWork;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using Swashbuckle.AspNetCore.Annotations;
using Dtos.GuestDtos;
using AutoMapper;
using WebApi.Filters;
using Business.Services.Abstract;
using WebApi.Responses;

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
    [SwaggerOperation(Summary = "Tüm misafirleri getirir", Description = "Veritabanındaki tüm misafirleri listeler.")]
    [SwaggerResponse(200, "Başarılı istek", typeof(List<GuestDto>))]
    public async Task<IActionResult> GetAllAsync()
    {
        var result = await _guestService.GetAllAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "ID'ye göre misafir getirir", Description = "Belirtilen ID'ye sahip misafiri getirir.")]
    [SwaggerResponse(200, "Misafir bulundu", typeof(GuestDto))]
    [SwaggerResponse(404, "Misafir bulunamadı")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var result = await _guestService.GetByIdAsync(id);

        return Ok(result);
    }
    [HttpGet("{id}/reservations")]
    [SwaggerOperation(Summary = "Misafirin rezervasyonlarını getirir", Description = "Belirtilen ID'ye sahip misafirin rezervasyon bilgileriyle birlikte döner.")]
    [SwaggerResponse(404, "Belirtilen ID'ye sahip misafir bulunamadı")]
    public async Task<IActionResult> GetWithReservationsByIdAsync(int id)
    {
        var result = await _guestService.GetWithReservationsAsync(id);
        return Ok(result);
    }
    [HttpGet("paged")]
    [SwaggerOperation(Summary = "Misafirleri sayfalayarak getirir", Description = "Belirtilen ID'ye sahip misafirin rezervasyon bilgileriyle birlikte döner.")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int currentPage, [FromQuery] int size)
    {
        var result = await _guestService.GetPagedAsync(currentPage, size);
        return Ok(result);
    }
    [HttpPost]
    [ServiceFilter(typeof(ValidationFilter<CreateGuestDto>))]
    [SwaggerOperation(Summary = "Yeni misafir oluşturur", Description = "Yeni bir misafir kaydı oluşturur.")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateGuestDto newGuestDto)
    {
        var result = await _guestService.CreateAsync(newGuestDto);
        return CreatedAtAction(nameof(GetByIdAsync), "Guests", new { id = result.Data.Id}, result);
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationFilter<UpdateGuestDto>))]
    [SwaggerOperation(Summary = "Misafir bilgilerini günceller", Description = "Verilen ID'deki misafirin bilgilerini günceller.")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateGuestDto updateGuestDto)
    {
        var result = await _guestService.UpdateAsync(id, updateGuestDto);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Misafiri siler", Description = "Belirtilen ID'deki misafiri siler.")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        await _guestService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("search")]
    [SwaggerOperation(Summary = "Misafir arar", Description = "İsim, email veya telefon bilgisine göre misafir arar.")]
    public async Task<IActionResult> SearchAsync([FromQuery] string? query)
    {
        var result = await _guestService.SearchAsync(query);
        return Ok(result);
    }

}
