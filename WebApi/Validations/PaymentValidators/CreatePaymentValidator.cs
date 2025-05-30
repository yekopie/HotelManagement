using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.PaymentDtos;

namespace Business.ValidatonRules.PaymentValidationRules
{
    public class CreatePaymentDtoValidator : AbstractValidator<CreatePaymentDto>
    {
        public CreatePaymentDtoValidator()
        {
          RuleFor(x => x.ReservationId)
                .NotEmpty().WithMessage("Rezervasyon ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Rezervasyon ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.Amount)
                .NotEmpty().WithMessage("Ödeme tutarı zorunludur.")
                .GreaterThan(0).WithMessage("Ödeme tutarı 0'dan büyük olmalıdır.");
            RuleFor(x => x.Method)
                .NotEmpty().WithMessage("Ödeme yöntemi zorunludur.")
                .Length(2, 50).WithMessage("Ödeme yöntemi 2 ile 50 karakter arasında olmalıdır.");
            RuleFor(x => x.PaidAt)
                .NotEmpty().WithMessage("Ödeme tarihi zorunludur.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ödeme tarihi bugünden önce veya eşit olmalıdır.");
            RuleFor(x => x.IsSuccessful)
                .NotNull().WithMessage("Ödeme durumu zorunludur.");
        }
    }



}
