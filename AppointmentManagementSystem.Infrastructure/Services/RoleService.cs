using AppointmentManagementSystem.Application.Common.Interfaces;
using AppointmentManagementSystem.Application.DTOs.Roles;
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
