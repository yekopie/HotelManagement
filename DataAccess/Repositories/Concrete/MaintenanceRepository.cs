using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class MaintenanceRepository : RepositoryBase<Maintenance>, IMaintenanceRepository
    {
        public MaintenanceRepository(AppDbContext context) : base(context)
        {
        }
    }
}
