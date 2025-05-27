using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class ReservationRepository : RepositoryBase<Guest>, IReservationRepository
    {
    }
}
