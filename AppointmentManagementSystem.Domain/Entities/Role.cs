using AppointmentManagementSystem.Domain.Common;
using AppointmentManagementSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Entities
{
    public class Role : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; private set; } = new List<UserRole>();
    }
}
