using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Roles;
using AppointmentManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppointmentManagementSystem.Infrastructure.Services
{
    public class RoleService : IRoleService
    {
        private readonly IApplicationDbContext _context;

        public RoleService(IApplicationDbContext context)
        {
            _context = context;
        }

         
        public async Task AssignUserRoleAsync(User user, Guid roleId, CancellationToken cancellationToken)
        {
            var userRole = new UserRole
            {
                UserId = user.Id,
                RoleId = roleId
            };

            await _context.UserRoles.AddAsync(userRole, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            return await _context.Roles
                .Where(r => !r.IsDeleted)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description
                })
                .ToListAsync();
        }
    }
}
