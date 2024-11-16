using AppointmentManagementSystem.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.UpdateAppointment
{
    public class UpdateAppointmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public DateTime AppointmentDate { get; set; }
        public Guid ServiceId { get; set; }
        public string? Notes { get; set; }
        public AppointmentStatus Status { get; set; }
    }
}
