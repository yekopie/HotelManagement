using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.Dtos.MaintenanceDtos;

namespace Business.ValidatonRules.MaintenanceValidationRules
{
    public class UpdateMaintenanceDtoValidator:AbstractValidator<UpdateMaintenanceDto>
    {
        public UpdateMaintenanceDtoValidator() 
        {
           
            RuleFor(x => x.RoomId)
                .NotEmpty().WithMessage("Oda ID'si zorunludur.")
                .GreaterThan(0).WithMessage("Oda ID'si 0'dan büyük olmalıdır.");
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Bakım türü zorunludur.")
                .Length(2, 50).WithMessage("Bakım türü 2 ile 50 karakter arasında olmalıdır.");
            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Bakım durumu zorunludur.")
                .Length(2, 20).WithMessage("Bakım durumu 2 ile 20 karakter arasında olmalıdır.");
            RuleFor(x => x.ScheduledDate)
                .NotEmpty().WithMessage("Planlanan tarih zorunludur.")
                .GreaterThan(DateTime.Now).WithMessage("Planlanan tarih bugünden sonraki bir tarih olmalıdır.");
            RuleFor(x => x.CompletedDate)
                .GreaterThanOrEqualTo(x => x.ScheduledDate).WithMessage("Tamamlanan tarih, planlanan tarihten önce olamaz.")
                .When(x => x.CompletedDate != default(DateTime)); //tamamlanan tarih boş değilse kontrol et 
            RuleFor(x => x.Note)
                .MaximumLength(500).WithMessage("Not 500 karakterden uzun olmamalıdır.")
                .When(x => !string.IsNullOrEmpty(x.Note)); // Not alanı boş değilse kontrol et



        }
    }
}
