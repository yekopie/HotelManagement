
using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using Dtos.MaintenanceDtos;

namespace Business.Services.Concrete
{
    public class MaintanceService : IMaintanceService
    {
        public Task<IOutcome> CompleteMaintenanceAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<MaintenanceDto>> CreateMaintenanceAsync(CreateMaintenanceDto newMaintenance)
        {
            throw new NotImplementedException();
        }

        public Task<IDataOutcome<IEnumerable<MaintenanceDto>>> GetMaintenanceListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IOutcome> StartMaintenanceAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
