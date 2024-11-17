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

namespace AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointments
{
    public class GetAppointmentsQueryHandler : IRequestHandler<GetAppointmentsQuery, ResultDto<List<AppointmentDto>>>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUser _currentUser;
        private readonly IMapper _mapper;

        public GetAppointmentsQueryHandler(IApplicationDbContext context, ICurrentUser currentUser, IMapper mapper)
        {
            _context = context;
            _currentUser = currentUser;
            _mapper = mapper;
        }

        public async Task<ResultDto<List<AppointmentDto>>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Appointment> query = _context.Appointments
                .Include(a => a.Service)
                .Include(a => a.User);

            if (!_currentUser.IsAdmin)
            {
                query = query.Where(a => a.UserId == _currentUser.Id.Value);
            }

            var appointments = await query.OrderByDescending(a => a.AppointmentDate)
                .ToListAsync(cancellationToken);

            var response =  _mapper.Map<List<AppointmentDto>>(appointments);
            return ResultDto<List<AppointmentDto>>.Success(response, "İşlem Başarılı");
        }
    }
}
