using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Domain.Enums
{
    public enum UserRole
    {
        User = 0,
        Admin = 1
    }
    public enum AppointmentStatus
    {
        Pending = 0,
        Confirmed = 1,
        Cancelled = 2,
        Completed = 3
    }
}
