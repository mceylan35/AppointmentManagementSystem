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

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public CreateAppointmentCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ResultDto<bool>> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Appointment
            {
                AppointmentDate = request.AppointmentDate,
                ServiceId = request.ServiceId,
                UserId = _currentUser.Id.Value,
                Status = AppointmentStatus.Pending,
                Notes = request.Notes,
                CreatedBy = _currentUser.UserName,
                CreatedAt = DateTime.UtcNow
            };

            _context.Appointments.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);


            return ResultDto<bool>.Success(true, "Radevu başarıyla eklendi.");
        }
    }
}
