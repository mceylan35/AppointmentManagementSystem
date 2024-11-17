using AppointmentManagementSystem.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.CreateAppointment
{
    public class CreateAppointmentCommand : IRequest<ResultDto<bool>>
    {
        public DateTime AppointmentDate { get; set; }
        public Guid ServiceId { get; set; }
        public string Notes { get; set; }
    }
}
