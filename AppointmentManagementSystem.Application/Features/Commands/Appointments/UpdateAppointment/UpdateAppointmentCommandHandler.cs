﻿using AppointmentManagementSystem.Application.Common.Exceptions;
using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommandHandler : IRequestHandler<UpdateAppointmentCommand, ResultDto<bool>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;

        public UpdateAppointmentCommandHandler(IApplicationDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<ResultDto<bool>> Handle(UpdateAppointmentCommand request, CancellationToken cancellationToken)
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
            if (_currentUser.IsAdmin)
            {
                // Sadece admin statü güncelleyebilir
                entity.Status = request.Status;
            }
            else
            {
                entity.AppointmentDate = request.AppointmentDate;
                entity.ServiceId = request.ServiceId.Value;
                entity.Notes = request.Notes;
                
            } 
            var result= await _context.SaveChangesAsync(cancellationToken);

            return ResultDto<bool>.Success(true, "Radevu başarıyla güncellendi.");
        }
    }
}
