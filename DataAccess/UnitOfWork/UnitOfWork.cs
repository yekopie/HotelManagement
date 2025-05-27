using DataAccess.Context;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.UnitOfWork
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private bool _disposed = false;
        private AppDbContext _context = context;
        private IGuestRepository _guestRepository;
        private IHotelRepository _hotelRepository;
        private IMaintenanceRepository _maintenanceRepository;
        private IPaymentRepository _paymentRepository;
        private IReservationRepository _reservationRepository;
        private IRoomRepository _roomRepository;
        private IRoomTypeRepository _roomTypeRepository;

        public IGuestRepository GuestRepository => _guestRepository ??= new GuestRepository();

        public IHotelRepository HotelRepository => _hotelRepository ??= new HotelRepository();

        public IMaintenanceRepository MaintenanceRepository => _maintenanceRepository ??= new MaintenanceRepository();

        public IPaymentRepository PaymentRepository => _paymentRepository ??= new PaymentRepository();

        public IReservationRepository ReservationRepository => _reservationRepository ??= new ReservationRepository();

        public IRoomRepository RoomRepository => _roomRepository ??= new RoomRepository();

        public IRoomTypeRepository RoomTypeRepository => _roomTypeRepository ??= new RoomTypeRepository();

        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose();
                // Suppress finalization.
                GC.SuppressFinalize(this);
            }
            
            _disposed = true;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
