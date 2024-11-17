using AppointmentManagementSystem.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommandValidator : AbstractValidator<UpdateAppointmentCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateAppointmentCommandValidator(IApplicationDbContext context)
        {
            _context = context;
            
               
            RuleFor(v => v.ServiceId.Value)
                .NotEmpty().WithMessage("Hizmet seçimi zorunludur.")
                .MustAsync(BeValidService).WithMessage("Geçersiz hizmet seçimi.");

            RuleFor(v => v.Notes)
                .MaximumLength(500).WithMessage("Notlar 500 karakteri geçemez.");

            RuleFor(v => v.Status)
                .IsInEnum().WithMessage("Geçerli bir durum seçiniz.");
        }

        private async Task<bool> BeValidService(Guid serviceId, CancellationToken cancellationToken)
        {
            return await _context.Services.AnyAsync(s => s.Id == serviceId && s.IsActive);
        }
    }
}
