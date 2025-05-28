using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos;
using WebApi.Dtos.HotelDtos;
using Microsoft.EntityFrameworkCore;
using DataAccess.UnitOfWork;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GuestController : ControllerBase
{
        private IUnitOfWork _unitOfWork;

        public GuestController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var guests = await _unitOfWork.GuestRepository.GetAll().ToListAsync();
        return Ok(guests);
    }


}





