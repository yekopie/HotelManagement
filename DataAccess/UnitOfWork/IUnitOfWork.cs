using DataAccess.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGuestRepository GuestRepository { get; }
        IHotelRepository HotelRepository { get; }
        IMaintenanceRepository MaintenanceRepository { get; }
        IPaymentRepository PaymentRepository { get; }
        IReservationRepository ReservationRepository { get; }
        IRoomRepository RoomRepository { get; }
        IRoomTypeRepository RoomTypeRepository { get; }
        Task SaveChangesAsync();
    }
}
