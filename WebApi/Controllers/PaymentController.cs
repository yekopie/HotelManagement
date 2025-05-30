using AutoMapper;
using DataAccess.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Dtos.PaymentDtos;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentsController : ControllerBase
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper;

    public PaymentController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPaymentById([FromRoute] int id)
    {
        var payment = await _unitOfWork.PaymentRepository.GetByIdAsync(id);


        var paymentDto = _mapper.Map<PaymentDto>(payment);
        return Ok(paymentDto);

    }
    [HttpGet("reservation/{id}")]
    public async Task<IActionResult> GetPaymentByReservationId([FromRoute] int id)
    {

        var reservation = await _unitOfWork.ReservationRepository.GetByIdAsync(id);

        _unitOfWork.ReservationRepository.Update(reservation);
        await _unitOfWork.SaveChangesAsync();

        var paymentDtos = reservation.Payments.Select(p => _mapper.Map<PaymentDto>(p)).ToList();
        return Ok(paymentDtos);
    }


    
}
