using Dtos.RoomDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validation.RoomValidators
{
    public class UpdateRoomDtoValidator:AbstractValidator<UpdateRoomDto>
    {
        public UpdateRoomDtoValidator()
        { 
           
            RuleFor(x => x.HotelId)
                .NotEmpty().WithMessage("Otel ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Otel ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.RoomTypeId)
                .NotEmpty().WithMessage("Oda tipi ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Oda tipi ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.floor)
                .NotEmpty().WithMessage("Kat numarası zorunludur.")
                .GreaterThanOrEqualTo(0).WithMessage("Kat numarası 0 veya daha büyük olmalıdır.");
            RuleFor(x => x.RoomNumber)
                .NotEmpty().WithMessage("Oda numarası zorunludur.")
                .Matches(@"^\d+$").WithMessage("Oda numarası sadece rakamlardan oluşmalıdır.");
            RuleFor(x => x.PricePerNight)
                .NotEmpty().WithMessage("Gece başı fiyat zorunludur.")
                .GreaterThan(0).WithMessage("Gece başı fiyat 0'dan büyük olmalıdır.");
            RuleFor(x => x.IsAvailable)
                .NotNull().WithMessage("Oda durumu zorunludur.");
            RuleFor(x => x.IsClean)
                .NotNull().WithMessage("Temizlik durumu zorunludur.");


        }
    }
}
