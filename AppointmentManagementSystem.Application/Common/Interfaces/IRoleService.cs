using AppointmentManagementSystem.Application.DTOs.Roles;
using AppointmentManagementSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Application.Common.Interfaces
{
    public interface IRoleService
    {
        Task<List<RoleDto>> GetAllRolesAsync();
        Task AssignUserRoleAsync(User user, Guid roleId, CancellationToken cancellationToken);
    }
}
