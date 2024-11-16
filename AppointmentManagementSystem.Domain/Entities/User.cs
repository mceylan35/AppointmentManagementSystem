using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Entities
{
    public class User : BaseAuditableEntity
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; } 
        public bool IsActive { get; set; }
        public string? PhoneNumber { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
        public virtual ICollection<Appointment> Appointments { get; private set; } = new List<Appointment>();
    }
}
