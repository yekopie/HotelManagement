using Core.Utilities.Results.Abstract;
using Dtos.GuestDtos;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services.Abstract
{
    public interface IGuestService
    {
        Task<IDataOutcome<List<GuestDto>>> GetAllAsync();
        Task<IDataOutcome<GuestDto>> GetByIdAsync(int id);
        Task<IDataOutcome<GuestWithReservationsDto>> GetWithReservationsAsync(int id);
        Task<IDataOutcome<(List<GuestDto> Guests, int TotalCount)>> GetPagedAsync(int currentPage, int size);
        Task<IDataOutcome<GuestDto>> CreateAsync(CreateGuestDto createGuestDto);
        Task<IDataOutcome<GuestDto>> UpdateAsync(int id, UpdateGuestDto updateGuestDto);
        Task DeleteAsync(int id);
        Task<IDataOutcome<List<GuestDto>>> SearchAsync(string? query);
    }
}
