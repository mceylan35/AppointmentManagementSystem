using AppointmentManagementSystem.Application.Common.Exceptions;
using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, bool>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public UpdateAppointmentCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<bool> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            // Admin değilse sadece kendi randevularını güncelleyebilir
            if (!_currentUser.IsAdmin && entity.UserId != _currentUser.Id)
            {
                throw new UnauthorizedAccessException("Bu randevuyu güncelleme yetkiniz yok.");
            }

            entity.AppointmentDate = request.AppointmentDate;
            entity.ServiceId = request.ServiceId;
            entity.Notes = request.Notes;

            // Sadece admin statü güncelleyebilir
            if (_currentUser.IsAdmin)
            {
                entity.Status = request.Status;
            }

           var result= await _context.SaveChangesAsync(cancellationToken);

            return result>0;
        }
    }
}
