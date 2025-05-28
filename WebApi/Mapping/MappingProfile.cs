using AutoMapper;
using Entities.Concrete;
using WebApi.Dtos.GuestDtos;
using WebApi.Dtos.HotelDtos;
using WebApi.Dtos.MaintenanceDtos;
using WebApi.Dtos.PaymentDtos;
using WebApi.Dtos.ReservationDtos;
using WebApi.Dtos.RoomDtos;
using WebApi.Dtos.RoomTypeDtos;

namespace WebApi.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // For Hotel
            CreateMap<Hotel, HotelDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, UpdateHotelDto>().ReverseMap();

            // For Guest
            CreateMap<Guest, GuestDto>()
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
                .ForMember(dest => dest.Payments, opt => opt.MapFrom(src => src.Payments));
            CreateMap<Reservation, CreateReservationDto>().ReverseMap();
            CreateMap<Reservation, UpdateReservationDto>().ReverseMap();

            // For Room
            CreateMap<Room, RoomDto>()
                .ForMember(dest => dest.RoomTypeId, opt => opt.MapFrom(src => src.RoomTypeId))
                .ForMember(dest => dest.HotelId, opt => opt.MapFrom(src => src.HotelId))
                .ForMember(dest => dest.Reservations, opt => opt.MapFrom(src => src.Reservations))
                .ForMember(dest => dest.Maintenances, opt => opt.MapFrom(src => src.Maintenances));
            CreateMap<Room, CreateRoomDto>().ReverseMap();
            CreateMap<Room, UpdateRoomDto>().ReverseMap();

            // For RoomType
            CreateMap<RoomType, RoomTypeDto>()
                .ForMember(dest => dest.Rooms, opt => opt.MapFrom(src => src.Rooms));
            CreateMap<RoomType, CreateRoomTypeDto>().ReverseMap();
            CreateMap<RoomType, UpdateRoomTypeDto>().ReverseMap();

            
           


        }
    }
}
