using AppointmentManagementSystem.Application.Common.Exceptions;
using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using AppointmentManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommandHandler : IRequestHandler<DeleteAppointmentCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public DeleteAppointmentCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ResultDto<bool>> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Appointments
                .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

             //sadece kendi randevularını silebilir
            if (!_currentUser.IsAdmin && entity.UserId != _currentUser.Id)
            {
                throw new UnauthorizedAccessException("Bu randevuyu silme yetkiniz yok.");
            }

            entity.IsDeleted = true;
          
            // _context.Appointments.Remove(entity);
           

            var result=   await _context.SaveChangesAsync(cancellationToken);

            return ResultDto<bool>.Success(true, "Radevu başarıyla silindi.");
        }
    }
}