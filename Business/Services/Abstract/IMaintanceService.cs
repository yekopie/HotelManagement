using Core.Utilities.Results.Abstract;
using Dtos.MaintenanceDtos;
using System.Collections.Immutable;

namespace Business.Services.Abstract
{
    public interface IMaintanceService
    {
        Task<IDataOutcome<IEnumerable<MaintenanceDto>>> GetMaintenanceListAsync();
        Task<IDataOutcome<MaintenanceDto>> CreateMaintenanceAsync(CreateMaintenanceDto newMaintenance);
        Task<IOutcome> StartMaintenanceAsync(int id);
        Task<IOutcome> CompleteMaintenanceAsync(int id);
    }
}
