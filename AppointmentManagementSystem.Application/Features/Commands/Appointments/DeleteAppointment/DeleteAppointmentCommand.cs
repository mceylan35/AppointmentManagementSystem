﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Features.Commands.Appointments.DeleteAppointment
{
    public class DeleteAppointmentCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}