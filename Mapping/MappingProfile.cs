using AutoMapper;
using Dtos.GuestDtos;
using Dtos.HotelDtos;
using Dtos.MaintenanceDtos;
using Dtos.PaymentDtos;
using Dtos.ReservationDtos;
using Dtos.RoomDtos;
using Dtos.RoomTypeDtos;
using Entities.Concrete;

namespace Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For Hotel
            CreateMap<Hotel, HotelDto>();
            CreateMap<Hotel, HotelWithRoomsDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();

            // For Guest
            CreateMap<Guest, GuestDto>();
            CreateMap<Guest, GuestWithReservationsDto>()
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));
            CreateMap<Guest, CreateGuestDto>().ReverseMap();
            CreateMap<Guest, UpdateGuestDto>().ReverseMap();

            // For Maintenance
            CreateMap<Maintenance, MaintenanceDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId));
            CreateMap<Maintenance, CreateMaintenanceDto>().ReverseMap();
            CreateMap<Maintenance, UpdateMaintenanceDto>().ReverseMap();

            // For Payment
            CreateMap<Payment, PaymentDto>()
                .ForMember(dest => dest.ReservationId, opt => opt.MapFrom(src => src.ReservationId));
            CreateMap<Payment, CreatePaymentDto>().ReverseMap();
            CreateMap<Payment, UpdatePaymentDto>().ReverseMap();

            // For Reservation
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.RoomId, opt => opt.MapFrom(src => src.RoomId))
                .ForMember(dest => dest.Status, opt => 
                opt.MapFrom(src => src.Status.ToString())); // Enum -> String dönüşümü eklendi

            CreateMap<Reservation, ReservationWithPaymentsDto>()
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments))
                .ForMember(dest => dest.Status, opt =>
                opt.MapFrom(src => src.Status.ToString())); // Enum -> String dönüşümü eklendi

            CreateMap<Reservation, CreateReservationDto>().ReverseMap()
                .ForMember(dest => dest.Status, opt => 
                opt.MapFrom(src => Enum.Parse<ReservationStatus>(src.Status))); // String -> Enum dönüşümü eklendi

            CreateMap<Reservation, UpdateReservationDto>().ReverseMap()
                .ForMember(dest => dest.Status, opt => 
                opt.MapFrom(src => Enum.Parse<ReservationStatus>(src.Status))); // String -> Enum dönüşümü eklendi

            // For Room
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId));
            CreateMap<Room, RoomWithReservationsDto>()
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations));
            CreateMap<Room, RoomWithMaintenancesDto>()
                .ForMember(dest => dest.Maintenances, opt => opt.MapFrom(src => src.Maintenances));
            CreateMap<Room, CreateRoomDto>().ReverseMap();
            CreateMap<Room, UpdateRoomDto>().ReverseMap();

            // For RoomType
            CreateMap<RoomType, RoomTypeDto>();
            CreateMap<RoomType, RoomTypeWithRoomsDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<RoomType, CreateRoomTypeDto>().ReverseMap();
            CreateMap<RoomType, UpdateRoomTypeDto>().ReverseMap();





        }
    }
}
