using AppointmentManagementSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Entities
{
    public class Service : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; } 
        public bool IsActive { get; set; }

        public virtual ICollection<Appointment> Appointments { get;  set; } 
    }
}
