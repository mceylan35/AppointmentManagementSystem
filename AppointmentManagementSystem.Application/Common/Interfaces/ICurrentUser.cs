using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Common.Interfaces
{
    public interface ICurrentUser
    {
        Guid? Id { get; }
        string UserName { get; }
        bool IsAuthenticated { get; }
        bool IsAdmin { get; }
    }
}
