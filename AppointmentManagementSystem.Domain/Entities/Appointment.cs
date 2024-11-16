using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Entities
{
    public class Appointment : BaseAuditableEntity
    {
        public DateTime AppointmentDate { get; set; }
        public AppointmentStatus Status { get; set; }
        public string? Notes { get; set; } 
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public virtual User User { get; set; }
        public virtual Service Service { get; set; }
    }
}
