
using Core.Utilities.Results.Abstract;
using Dtos.ReservationDtos;
using System.Linq.Expressions;

namespace Business.Services.Abstract
{
    public interface IReservationService
    {
        //Task<IDataOutcome<IDataOutcome<IEnumerable<ReservationDto>>>> GetReservationsAsync(Expression<Func<ReservationFilter>> filter = null);
        Task<IDataOutcome<List<ReservationDto>>> GetAllReservationsAsync();
        Task<IDataOutcome<ReservationDto>> GetReservationByIdAsync(int id);
        Task<IDataOutcome<ReservationDto>> CreateReservationAsync(CreateReservationDto newReservation);
        Task<IOutcome> UpdateReservationAsync(int id, UpdateReservationDto updatedReservation);

        Task<IOutcome> ConfirmReservationAsync(int id);
        Task<IOutcome> CheckInReservationAsync(int id);
        Task<IOutcome> CheckOutReservationAsync(int id);

        Task DeleteReservationAsync(int id);
    }
}
