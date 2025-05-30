using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.HotelDtos;

namespace Business.ValidatonRules.HotelValdationRules
{
    public class CreatHotelDtoValidator : AbstractValidator<CreateHotelDto>
    {
        public CreatHotelDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Otel adı zorunludur.")
                .Length(2, 100).WithMessage("Otel adı 2 ile 100 karakter arasında olmalıdır.");
            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Adres zorunludur.")
                .Length(5, 200).WithMessage("Adres 5 ile 100 karakter arasında olmalıdır.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası zorunludur.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz. (10-15 haneli, + ile başlayabilir.)");
            RuleFor(x => x.City)
                    .NotEmpty().WithMessage("Şehir zorunludur.")
                    .Length(2, 50).WithMessage("Şehir adı 2 ile 13 karakter arasında olmalıdır.");
            RuleFor(x => x.StarRating)
                .InclusiveBetween(1, 5).WithMessage("Yıldız derecelendirmesi 1 ile 5 arasında olmalıdır.")
                .WithMessage("Yıldız derecelendirmesi 1 ile 5 arasında olmalıdır.");


        }
    }
}
