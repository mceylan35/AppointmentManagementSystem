using AppointmentManagementSystem.Application.Common.Exceptions;
using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Application.DTOs.Users;
using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Entities;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointmentDetail
{
    public class GetAppointmentDetailQueryHandler : IRequestHandler<GetAppointmentDetailQuery, ResultDto<AppointmentDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser _currentUser;

        public GetAppointmentDetailQueryHandler(
            IApplicationDbContext context,
            IMapper mapper,
            ICurrentUser currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<ResultDto<AppointmentDto>> Handle(GetAppointmentDetailQuery request, CancellationToken cancellationToken)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

            if (appointment == null)
            {
                throw new NotFoundException(nameof(Appointment), request.Id);
            }

            // Admin değilse sadece kendi randevularını görebilir
            if (!_currentUser.IsAdmin && appointment.UserId != _currentUser.Id)
            {
                throw new UnauthorizedAccessException("Bu randevuyu görüntüleme yetkiniz yok.");
            }

            var response = _mapper.Map<AppointmentDto>(appointment);
            return ResultDto<AppointmentDto>.Success(response, "İşlem Başarılı");

        }
    }
}
