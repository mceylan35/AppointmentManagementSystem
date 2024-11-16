using AppointmentManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommandValidator : AbstractValidator<CreateAppointmentCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateAppointmentCommandValidator(IApplicationDbContext context)
        {

            _context = context;

            RuleFor(v => v.AppointmentDate)
               .NotEmpty().WithMessage("Randevu tarihi zorunludur.")
               .GreaterThan(DateTime.Now).WithMessage("Randevu tarihi bugünden sonra olmalıdır.");

            RuleFor(v => v.ServiceId)
                .NotEmpty().WithMessage("Hizmet seçimi zorunludur.")
                .MustAsync(BeValidService).WithMessage("Geçersiz hizmet seçimi.");

            RuleFor(v => v.Notes)
                .MaximumLength(500).WithMessage("Notlar 500 karakteri geçemez.");
        }

        private async Task<bool> BeValidService(Guid serviceId, CancellationToken cancellationToken)
        {
            return await _context.Services.AnyAsync(s => s.Id == serviceId && s.IsActive);
        }
    }
}