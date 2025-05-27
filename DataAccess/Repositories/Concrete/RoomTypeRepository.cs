using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using Entities.Concrete;

namespace DataAccess.Repositories.Concrete
{
    public class RoomTypeRepository : RepositoryBase<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(AppDbContext context) : base(context)
        {
        }
    }
}
