using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.GuestDtos;

namespace Business.ValidatonRules.GuestValidationRules
{
    public class UpdateGuestDtoValidator:AbstractValidator<UpdateGuestDto>
    {
        public UpdateGuestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim boş olamaz.")
                .Length(2, 50).WithMessage("İsim 2 ile 50 karakter arasında olmalı.")
                .When(x => x.FirstName is not null);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim boş olamaz.")
                .Length(2, 50).WithMessage("Soyisim 2 ile 50 karakter arasında olmalı.")
                .When(x => x.LastName is not null);

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.")
                .When(x => x.Email is not null);

            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage("Telefon numarası boş olamaz.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz. (10-15 haneli, + ile başlayabilir.)")
                .When(x => x.Phone is not null);
        }
    }
}
