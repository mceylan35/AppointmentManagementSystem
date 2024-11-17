using AppointmentManagementSystem.Application.DTOs;
using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Queries.Appointments.GetAppointments
{
    public class GetAppointmentsQuery : IRequest<ResultDto<List<AppointmentDto>>>
    {
       
    }
}
