﻿using Dtos.GuestDtos;
using FluentValidation;


namespace Validation.GuestValidators
{
    public class CreateGuestDtoValidator:AbstractValidator<CreateGuestDto>
    {
      public CreateGuestDtoValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("İsim zorunludur")
                .Length(2, 50).WithMessage("İsim 2 ile 20 karakter arasında olmalı.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Soyisim zorunludur")
                .Length(2, 50).WithMessage("Soyisim 2 ile 20 karakter arasında olmalı.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email zorunludur")
                .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
            RuleFor(x => x.Phone)
                .NotEmpty().WithMessage(
                "Telefon numarası zorunludur")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Geçerli bir telefon numarası giriniz. (10-15 haneli, + ile başlayabilir.)");
            
        }
    }

   
}
