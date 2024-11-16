using AppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Events
{
    public class AppointmentCreatedEvent
    {
        public Appointment Appointment { get; }

        public AppointmentCreatedEvent(Appointment appointment)
        {
            Appointment = appointment;
        }
    }
}
