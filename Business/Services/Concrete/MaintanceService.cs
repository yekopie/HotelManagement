
using AutoMapper;
using Business.Services.Abstract;
using Core.Utilities.Results.Abstract;
using DataAccess.UnitOfWork;
using Dtos.MaintenanceDtos;

namespace Business.Services.Concrete
{
    public class MaintanceService : IMaintanceService
    {
        //TODO: Rezervasyonla çakışan bakım kaydedilmez.
        //TODO: Temizlik tipindeki kayıt tamamlanınca oda IsClean true yapılır.
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MaintanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
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
