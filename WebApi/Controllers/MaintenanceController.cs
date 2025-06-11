using AutoMapper;
using DataAccess.UnitOfWork;
using Dtos.MaintenanceDtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenancesController : ControllerBase
{
    //TODO: Servis katmanına çekilecek
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public MaintenancesController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var maintenances = await _unitOfWork.MaintenanceRepository.GetAllAsync();
        return Ok(maintenances);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        if (maintenance == null)
        {
            return NotFound();
        }
        var result = _mapper.Map<MaintenanceDto>(maintenance);
        return Ok(result);

    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateMaintenanceDto maintenanceDto)
    {
        //Rezervasyon Çakışması kontrolü
        var existingMaintenance = await _unitOfWork.MaintenanceRepository.GetQueryable()
            .AnyAsync(m => m.RoomId == maintenanceDto.RoomId && m.Status == "Scheduled" &&
                           m.ScheduledDate <= maintenanceDto.ScheduledDate &&
                           m.CompletedDate >= maintenanceDto.ScheduledDate);
        if (existingMaintenance)
        {
            return BadRequest("Bu tarihte zaten planlanmış bir bakım var.");
        }
        //yeni bakım kaydı oluşturma
        var maintenance = _mapper.Map<Entities.Concrete.Maintenance>(maintenanceDto);
        await _unitOfWork.MaintenanceRepository.CreateAsync(maintenance);
        await _unitOfWork.SaveChangesAsync();
        //eğer bakım tip temizlik ise, odanın temizlenmiş olarak işaretlenmesi
        if (maintenance.Type == "Temizlik")
        {
            var room = await _unitOfWork.RoomRepository.GetByIdAsync(maintenance.RoomId);
            if (room == null)
            {
                return NotFound("Oda bulunamadı.");
            }


            if (room != null)
            {
                room.IsClean = true;
                _unitOfWork.RoomRepository.Update(room);
                await _unitOfWork.SaveChangesAsync();
            }
        }
        return Ok(new { id = maintenance.Id, maintenanceDto });

    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] MaintenanceDto maintenanceDto)
    {
        if (id != maintenanceDto.Id)
        {
            return BadRequest("Kimlik uyuşmazlığı");
        }
        var existingMaintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        if (existingMaintenance == null)
        {
            return NotFound();
        }
        // Rezervasyon Çakışması kontrolü
        var conflictingMaintenance = await _unitOfWork.MaintenanceRepository.GetQueryable()
            .AnyAsync(m => m.RoomId == maintenanceDto.RoomId && m.Status == "Scheduled" &&
                           m.ScheduledDate <= maintenanceDto.ScheduledDate &&
                           m.CompletedDate >= maintenanceDto.ScheduledDate && m.Id != id);
        if (conflictingMaintenance)
        {
            return BadRequest("Bu tarihte zaten planlanmış bir bakım var.");
        }
        // Güncelleme işlemi
        var maintenance = _mapper.Map<Entities.Concrete.Maintenance>(maintenanceDto);
        _unitOfWork.MaintenanceRepository.Update(maintenance);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {
        var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        if (maintenance == null)
        {
            return NotFound();
        }
        _unitOfWork.MaintenanceRepository.Delete(maintenance);
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }

}
