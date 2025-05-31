using AutoMapper;
using Core.Utilities.Business;
using Core.Utilities.Exceptions;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.UnitOfWork;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebApi.Dtos.ReservationDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public ReservationsController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var reservations = await _unitOfWork.ReservationRepository.GetAllAsync();
        var reservationDtos = _mapper.Map<List<ReservationDto>>(reservations);
        return Ok(reservationDtos);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);

        var result = BusinessRules.Run(IfReservationIsNotNull(reservation));

        if (!result.IsSuccessfull())
        {
            throw new DomainRuleException(result.Message);
        }
        var reservationDto = _mapper.Map<ReservationDto>(reservation);
        return Ok(reservationDto);
    }

    [HttpPost]
    public async Task<IActionResult> AddReservationAsync([FromBody] CreateReservationDto reservationDto)
    {
        var reservation = _mapper.Map<Reservation>(reservationDto);
        await _unitOfWork.ReservationRepository.CreateAsync(reservation);
        await _unitOfWork.SaveChangesAsync();
        var reservationInfo = _mapper.Map<ReservationDto>(reservation);

        return Ok(reservationInfo);
    }

    private IOutcome IfReservationIsNotNull(Reservation? reservation)
    {
        return (reservation != null) ? new SuccessOutcome() : new ErrorOutcome("Böyle bir rezervasyon yok");
    }


    //private IOutcome IfReservationExists()
}
