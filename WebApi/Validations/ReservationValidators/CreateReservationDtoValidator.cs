using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.ReservationDtos;

namespace Business.ValidatonRules.ReservationValidationRules
{
    public class CreateReservationDtoValidator:AbstractValidator<CreateReservationDto>
    {
        public CreateReservationDtoValidator()
        {
            RuleFor(x=> x.RoomId)
                .NotEmpty().WithMessage("Oda ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Oda ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.GuestId)
                .NotEmpty().WithMessage("Misafir ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Misafir ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.TotalAmount)
                .NotEmpty().WithMessage("Toplam tutar zorunludur.")
                .GreaterThan(0).WithMessage("Toplam tutar 0'dan büyük olmalıdır.");
            RuleFor(x => x.CheckInDate)
                .NotEmpty().WithMessage("Giriş tarihi zorunludur.")
                .GreaterThanOrEqualTo(DateTime.Now).WithMessage("Giriş tarihi bugünden önce olamaz.");
            RuleFor(x => x.CheckOutDate)
                .NotEmpty().WithMessage("Çıkış tarihi zorunludur.")
                .GreaterThan(x => x.CheckInDate).WithMessage("Çıkış tarihi giriş tarihinden sonra olmalıdır.");
            RuleFor(x => x.Status)
                    .NotEmpty().WithMessage("Rezervasyon durumu zorunludur.")
                    .IsInEnum().WithMessage("Geçersiz rezervasyon durumu.");







        }
    }
}
