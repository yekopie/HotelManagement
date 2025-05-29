using AutoMapper;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos.MaintenanceDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MaintenancesController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
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
        return Ok();
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var maintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        if (maintenance == null)
        {
            return NotFound();
        }
        var maintanceDto = _mapper.Map<MaintenanceDto>(maintenance);
        
        return Ok(maintanceDto);
    }
    [HttpPost]
    public async Task<IActionResult> AddMaintenanceAsync([FromBody] CreateMaintenanceDto maintenance)
    {

        await _unitOfWork.SaveChangesAsync();
        return CreatedAtAction(nameof(GetByIdAsync), maintenance);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMaintenanceAsync([FromRoute] int id, [FromBody] UpdateMaintenanceDto maintenance)
    {
        /*if (id != maintenance.)
        {
            return BadRequest("Maintenance ID mismatch.");
        }*/
        var UpdateMaintenance = await _unitOfWork.MaintenanceRepository.GetByIdAsync(id);
        if (UpdateMaintenance == null)
        {
            return NotFound();
        }
        //_unitOfWork.MaintenanceRepository.Update(maintenance);
        await _unitOfWork.SaveChangesAsync();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMaintenanceAsync([FromRoute] int id)
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
